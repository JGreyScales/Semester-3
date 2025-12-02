#include <winsock2.h>
#include <iostream>
#include "../commands.h"

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
    char recvBuffer[1024]; // A buffer to store the received string

    // Receive data from the client
    int bytesReceived = recv(ConnectionSocket, recvBuffer, sizeof(recvBuffer), 0);
    if (bytesReceived == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to receive data" << std::endl;
        closesocket(ConnectionSocket);
        closesocket(ServerSocket);
        WSACleanup();
        return 0;
    }

    cout << "SUCCESS: Recieved Data" << std::endl;

    // Null-terminate the received string and print it
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    cout << "Received from client: " << recvBuffer << endl;

    // Send
    std::string result = CONSTRUCT_SUBMIT_SINGLE_POST("author 1", "topic 1", "body 1");
    char const * c_result = result.c_str();
    int sendResult = send(ConnectionSocket, c_result, strlen(c_result), 0);
    if (sendResult == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to send data to client" << std::endl;
        closesocket(ConnectionSocket);
        closesocket(ServerSocket);
        WSACleanup();
        return 0;
    }

    cout << "SUCCESS: Sent data to client" << std::endl;

    // Cleanup
    closesocket(ConnectionSocket);
    closesocket(ServerSocket);
    WSACleanup();

    return 0;
}
