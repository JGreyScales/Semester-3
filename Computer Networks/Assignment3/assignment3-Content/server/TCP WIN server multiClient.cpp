#include <iostream>
#include "client.h"
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
    bool serverShutdown = false;
    int clientIDTicker = 0;
    while (!serverShutdown)
    {

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
        clientIDTicker++;

        std::thread clientThread(clientHandler, &serverShutdown, ConnectionSocket, clientIDTicker);
        // need to dettach the thread to allow this line to run again without error
        clientThread.detach();
    }
    // Cleanup
    // tell all threads to shut down on next pass
    serverShutdown = true;
    closesocket(ServerSocket);
    WSACleanup();

    return 0;
}
