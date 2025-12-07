#include <winsock2.h>
#include <thread>
#include "userAuth.h"


bool sendToClient(std::string *message, int clientID, SOCKET ConnectionSocket)
{
    const char *c_result = (*message).c_str();
    std::cout << "Message to send: " << c_result << std::endl;
    int sendResult = send(ConnectionSocket, c_result, strlen(c_result), 0);
    if (sendResult == SOCKET_ERROR)
    {
        std::cout << "ERROR: Failed to send data to client " << clientID << std::endl;
        closesocket(ConnectionSocket);
        return 2;
    }
    std::cout << "SUCCESS: Sent data to client " << clientID << std::endl;
    return true;
}

// 0 == success
// 1 == client disconnected incorrectly
// 2 == unable to send to client
int clientHandler(bool *abortAllConnections, SOCKET ConnectionSocket, int clientID)
{
    std::cout << "SUCCESS: Accepted connection to client: " << clientID << std::endl;
    std::cout << "Running on thread: " << std::this_thread::get_id() << std::endl;
    char recvBuffer[BUFFERSIZE]; // A buffer to store the received string
    bool clientStopFlag = true;
    bool userAuthed = false;
    while (clientStopFlag && !*abortAllConnections)
    {
        // Receive data from the client
        int bytesReceived = recv(ConnectionSocket, recvBuffer, sizeof(recvBuffer), 0);

        if (bytesReceived > 0)
        {
            // Null-terminate the received string and print it
            recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
            std::cout << "Received from client " << clientID << ": " << recvBuffer << std::endl;
            std::string stringBuffer = recvBuffer;
            std::string status;

            if (!userAuthed)
            {
                int commandID = ID_COMMAND(&stringBuffer);
                if (4 != commandID && 5 != commandID)
                {
                    std::cout << "User was denied access to command: " << commandID << std::endl;
                    std::string message = "404 not authorized";
                    sendToClient(&message, clientID, ConnectionSocket);
                    continue;
                }

                std::string combo = extractUserPassCombo(&stringBuffer);
                if (combo == "")
                {
                    std::cout << "Invalid data was sent to server" << std::endl;
                    continue;
                }

                switch (commandID)
                {
                case 5:
                {
                    if (!userSignUp(&combo))
                    {
                        std::cout << "Unable to sign up user" << std::endl;
                        continue;
                    };
                    // we dont have a break here because we want to try signing
                    // in after signing the user up

                    // proper name is a "fall through" case
                }

                case 4:
                {

                    userAuthed = userSignInSuccess(combo);
                    std::string message = "auth status of user: " + std::to_string(userAuthed);
                    sendToClient(&message, clientID, ConnectionSocket);
                    break;
                }

                default:
                    break;
                }
            }
            else
            {

                switch (ID_COMMAND(&stringBuffer))
                {
                case 1:
                {
                    status = " STATUS: error";
                    std::cout << "Command SUBMIT_SINGLE_POST id found" << std::endl;
                    std::list<std::string> data = EXTRACT_ALL_DATA(&stringBuffer);
                    for (std::string value : data)
                    {
                        std::cout << value << " ";
                    }
                    std::cout << std::endl;
                    if (appendToFile(data, &POSTFILE))
                    {
                        status = " STATUS: success";
                    }
                    break;
                }
                case 2:
                {
                    status = " STATUS: error";
                    std::cout << "Command SUBMIT_MULTI_POST id found" << std::endl;
                    std::list<std::string> data = EXTRACT_ALL_DATA(&stringBuffer);
                    // each new POST is 3 items long
                    int ticker = 0;
                    for (std::string value : data)
                    {
                        if (ticker % 3 == 0)
                        {
                            std::cout << std::endl;
                            std::cout << "Post " << 1 + (ticker / 3) << ":";
                        }
                        std::cout << " " << value;
                        ticker++;
                    }
                    std::cout << std::endl;
                    if (appendToFile(data, &POSTFILE))
                    {
                        status = " STATUS: success";
                    }
                    else
                    {
                        std::cout << "Failed to save to file" << std::endl;
                    }
                    break;
                }
                case 3:
                {
                    status = " STATUS: error";
                    std::cout << "Command GET_ALL_POSTS id found" << std::endl;
                    std::list<std::string> packets;
                    std::string lastKnownGoodPacket;
                    std::string packetContent;

                    int ticker = 1;
                    for (std::string value : readFromFile(&POSTFILE))
                    {
                        packetContent = packetContent + value + delimiter;
                        // start of a new 3-item packet
                        if (ticker % 3 == 0)
                        {
                            if (lastKnownGoodPacket.length() + packetContent.length() >= BUFFERSIZE)
                            {
                                packets.insert(packets.end(), lastKnownGoodPacket);
                                lastKnownGoodPacket = packetContent;
                            }
                            else
                            {
                                lastKnownGoodPacket = lastKnownGoodPacket + packetContent;
                            }
                            packetContent.clear();
                        }
                        ticker++;
                    }
                    // ensure no partials are submitted
                    if (lastKnownGoodPacket.length() > 0)
                    {
                        packets.insert(packets.end(), lastKnownGoodPacket);
                    }
                    std::cout << "Sending " << packets.size() << " packet(s)" << std::endl;

                    for (std::string packet : packets)
                    {
                        const char *c_packet = packet.c_str();
                        int sendResult = send(ConnectionSocket, c_packet, strlen(c_packet), 0);
                        if (sendResult == SOCKET_ERROR)
                        {
                            std::cout << "ERROR: Failed to send data to client " << clientID << std::endl;
                            closesocket(ConnectionSocket);
                            return 2;
                        }
                        std::cout << "SUCCESS: Packet sent to client " << clientID << std::endl;
                    }
                    status = " STATUS: success";
                    break;
                }
                default:
                    break;
                }

                std::string result = "\nFIN: message received" + status;
                sendToClient(&result, clientID, ConnectionSocket);
            }
        }
        else if (bytesReceived == 0)
        {
            // Graceful disconnect
            std::cout << "Client " << clientID << " disconnected gracefully." << std::endl;
            closesocket(ConnectionSocket);
            return 0;
        }
        else
        { // bytesReceived == SOCKET_ERROR
            int err = WSAGetLastError();
            closesocket(ConnectionSocket);
            if (err == WSAECONNABORTED)
            {
                // Client forcibly closed the connection
                std::cout << "Client " << clientID << " disconnected forcibly." << std::endl;
                return 0;
            }
            else
            {
                std::cout << "recv failed with error: " << err << std::endl;
                std::cout << "Issue likely stems from client " << clientID << " disconnecting incorrectly" << std::endl;
                return 1;
            }
        }
    }
    return 0;
}