#include "client.h"

// Link the Winsock library
#pragma comment(lib, "ws2_32.lib")

int main()
{
    // Initialize Winsock
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
    {
        std::cout << "ERROR: Failed to start WSA" << std::endl;
        return 0;
    }

    std::cout << "SUCCESS: Started WSA" << std::endl;

    // Create the socket
    SOCKET ClientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ClientSocket == INVALID_SOCKET)
    {
        WSACleanup();
        std::cout << "ERROR: Failed to create ClientSocket" << std::endl;
        return 0;
    }

    std::cout << "SUCCESS: Started ClientSocket" << std::endl;

    // Set up the server address structure
    sockaddr_in SvrAddr;
    int port = 27000;
    const char *IP = "127.0.0.1";
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_port = htons(port);          // Convert port to network byte order
    SvrAddr.sin_addr.s_addr = inet_addr(IP); // Localhost address

    // Attempt to connect to the server
    if (connect(ClientSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == SOCKET_ERROR)
    {
        closesocket(ClientSocket);
        WSACleanup();
        std::cout << "ERROR: Connection attempt failed " << IP << ':' << port << std::endl;
        return 0;
    }

    std::cout << "SUCCESS: Successfully connected to the server at " << IP << ':' << port << std::endl;


    int result = clientSession(ClientSocket);


    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}