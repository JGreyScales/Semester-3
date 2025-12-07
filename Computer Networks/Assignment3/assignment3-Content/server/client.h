#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include <thread>
#include "fileSaving.h"
#include "commands.h"

// 0 == success
// 1 == client disconnected incorrectly
// 2 == unable to send to client
int clientHandler(bool* abortAllConnections, int ConnectionSocket, int clientID)
{
    std::cout << "SUCCESS: Accepted connection to client: " << clientID << std::endl;
    std::cout << "Running on thread: " << std::this_thread::get_id() << std::endl;
    char recvBuffer[BUFFERSIZE]; // A buffer to store the received string
    bool clientStopFlag = true;
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
            switch (ID_COMMAND(&stringBuffer))
            {
                /*
                * The Server assumes it can only send a "success" response for a post (Commands 1 or 2) after successfully saving and
                closing the updated post data in the persistent file (posts.txt). If the file operation fails for any reason, 
                the Server is programmed to immediately send an "error" status to the Client.
                This guarantees the Server maintains data integrity and never lies to the Client about successfully saving a post.
                */
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
                if (appendToPosts(data))
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
                if (appendToPosts(data))
                {
                    status = " STATUS: success";
                } else {
                    std::cout << "Failed to save to file" << std::endl;
                }
                break;
            }

            /*
               When responding to the GET_ALL_POSTS request (Command 3), the Server assumes it must maintain packet integrity.
               This means that if the Server's output stream hits its network buffer limit,
               it must not break a single post (Author:Topic:Body) between two different packets.
               The Server's logic ensures that any packet split occurs only after a complete,
               3-field post is fully assembled, preventing the Client's parsing functions from failing on integration.
             */

            case 3:
            {
                status = " STATUS: error";
                std::cout << "Command GET_ALL_POSTS id found" << std::endl;
                std::list<std::string> packets;
                std::string lastKnownGoodPacket;
                std::string packetContent;

                int ticker = 1;
                for (std::string value : readFromPostsFile())
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
                    if (sendResult < 0)
                    {
                        std::cout << "ERROR: Failed to send data to client " << clientID << std::endl;
                        close(ConnectionSocket);
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
            const char *c_result = result.c_str();
            int sendResult = send(ConnectionSocket, c_result, strlen(c_result), 0);
            if (sendResult < 0)
            {
                std::cout << "ERROR: Failed to send data to client " << clientID << std::endl;
                close(ConnectionSocket);
                return 2;
            }
            std::cout << "SUCCESS: Sent data to client " << clientID << std::endl;
        }
        else if (bytesReceived == 0)
        {
            // Graceful disconnect
            std::cout << "Client " << clientID << " disconnected gracefully." << std::endl;
            close(ConnectionSocket);
            return 0;
        }
        else
        { // bytesReceived < 0
            close(ConnectionSocket);
            // Client forcibly closed the connection
            std::cout << "Client " << clientID << " disconnected." << std::endl;
            return 0;
        }
    }
    return 0;
}