#include <winsock2.h>
#include <iostream>
#include "../commands.h"
#include "fileSaving.h"

using namespace std;

// Link the Winsock library
#pragma comment(lib, "ws2_32.lib")

int main()
{
    // Startup
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
    {
        cout << "ERROR: Failed to start WSA" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started WSA" << std::endl;

    // Socket
    SOCKET ServerSocket;
    ServerSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ServerSocket == INVALID_SOCKET)
    {
        cout << "ERROR: Failed to create ServerSocket" << std::endl;
        WSACleanup(); // Clean up Winsock if socket creation fails
        return 0;
    }

    cout << "SUCCESS: Started ServerSocket" << std::endl;

    // Bind
    sockaddr_in SvrAddr;
    int port = 27000;
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_addr.s_addr = INADDR_ANY;
    SvrAddr.sin_port = htons(port);
    if (bind(ServerSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == SOCKET_ERROR)
    {
        closesocket(ServerSocket); // Close the socket on error
        WSACleanup();              // Clean up Winsock
        cout << "ERROR: Failed to bind ServerSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Binded ServerSocket to port " << port << std::endl;

    // Listen
    if (listen(ServerSocket, 1) == SOCKET_ERROR)
    {
        closesocket(ServerSocket); // Close the socket on error
        WSACleanup();              // Clean up Winsock
        cout << "ERROR: Listen failed to configure ServerSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Listening on ServerSocket" << std::endl;

    // Accept
    SOCKET ConnectionSocket;
    ConnectionSocket = accept(ServerSocket, NULL, NULL);
    if (ConnectionSocket == SOCKET_ERROR)
    {
        closesocket(ServerSocket); // Close the server socket on error
        WSACleanup();              // Clean up Winsock
        cout << "ERROR: Failed to accept connection" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Accepted connection" << std::endl;
    // Recieve
    // Buffer to store received data
    char recvBuffer[BUFFERSIZE]; // A buffer to store the received string
    bool clientStopFlag = true;
    while (clientStopFlag)
    {

        // Receive data from the client
        int bytesReceived = recv(ConnectionSocket, recvBuffer, sizeof(recvBuffer), 0);

        if (bytesReceived > 0)
        {
            // Null-terminate the received string and print it
            recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
            cout << "Received from client: " << recvBuffer << endl;
            std::string stringBuffer = recvBuffer;
            std::string status;
            switch (ID_COMMAND(&stringBuffer))
            {
            case 1:
            {
                status = " STATUS: error";
                cout << "Command SUBMIT_SINGLE_POST id found" << endl;
                std::list<std::string> data = EXTRACT_ALL_DATA(&stringBuffer);
                for (std::string value : data)
                {
                    cout << value << " ";
                }
                cout << endl;
                if (appendToPosts(data))
                {
                    status = " STATUS: success";
                }
                break;
            }
            case 2:
            {
                status = " STATUS: error";
                cout << "Command SUBMIT_MULTI_POST id found" << endl;
                std::list<std::string> data = EXTRACT_ALL_DATA(&stringBuffer);
                // each new POST is 3 items long
                int ticker = 0;
                for (std::string value : data)
                {
                    if (ticker % 3 == 0)
                    {
                        cout << endl;
                        cout << "Post " << 1 + (ticker / 3) << ":";
                    }
                    cout << " " << value;
                    ticker++;
                }
                cout << endl;
                if (appendToPosts(data))
                {
                    status = " STATUS: success";
                }
                break;
            }
            case 3:
            {
                status = " STATUS: error";
                cout << "Command GET_ALL_POSTS id found" << endl;
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
                    if (sendResult == SOCKET_ERROR)
                    {
                        cout << "ERROR: Failed to send data to client" << std::endl;
                        closesocket(ConnectionSocket);
                        return 0;
                    }
                    cout << "SUCCESS: Packet sent to client" << std::endl;
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
            if (sendResult == SOCKET_ERROR)
            {
                cout << "ERROR: Failed to send data to client" << std::endl;
                closesocket(ConnectionSocket);
                return 0;
            }
            cout << "SUCCESS: Sent data to client" << std::endl;
        }
        else if (bytesReceived == 0)
        {
            // Graceful disconnect
            cout << "Client disconnected gracefully." << endl;
            closesocket(ConnectionSocket);
            clientStopFlag = false;
        }
        else
        { // bytesReceived == SOCKET_ERROR
            int err = WSAGetLastError();
            if (err == WSAECONNABORTED)
            {
                // Client forcibly closed the connection
                cout << "Client disconnected forcibly." << endl;
            }
            else
            {
                cout << "recv failed with error: " << err << endl;
            }
            closesocket(ConnectionSocket);
            clientStopFlag = false;
        }
    }
    // Cleanup
    closesocket(ConnectionSocket);
    closesocket(ServerSocket);
    WSACleanup();

    return 0;
}
