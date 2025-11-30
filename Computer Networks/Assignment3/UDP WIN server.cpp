#include <iostream>
#include <cstring>
#include <winsock2.h>
#include <ws2tcpip.h>

int main()
{
    // Initialize Winsock
    WSADATA wsaData;
    int wsaInit = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (wsaInit != 0)
    {
        std::cerr << "ERROR: WSAStartup failed with error code " << wsaInit << std::endl;
        return 1;
    }

    std::cout << "SUCCESS: WSAstartup" << std::endl;

    // Create Socket for UDP
    int ServerSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP); // Use IPPROTO_UDP for UDP
    if (ServerSocket == INVALID_SOCKET)
    {
        std::cerr << "ERROR: Failed to create ServerSocket" << std::endl;
        WSACleanup();
        return 1;
    }

    std::cout << "SUCCESS: Started ServerSocket" << std::endl;

    // Bind the socket to a port
    sockaddr_in SvrAddr;
    int port = 27500;
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_addr.s_addr = INADDR_ANY;  // Listen on any available interface
    SvrAddr.sin_port = htons(port);

    if (bind(ServerSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == SOCKET_ERROR)
    {
        std::cerr << "ERROR: Failed to bind ServerSocket" << std::endl;
        closesocket(ServerSocket);  // Close the socket on error
        WSACleanup();
        return 1;
    }

    std::cout << "SUCCESS: Binded ServerSocket to port " << port << std::endl;

    // Prepare buffer for receiving data
    char recvBuffer[1024];
    sockaddr_in ClientAddr;  // Store the client address so we can send data back
    socklen_t clientAddrLen = sizeof(ClientAddr);

    // Receive data from any client (no connection needed for UDP)
    int bytesReceived = recvfrom(ServerSocket, recvBuffer, sizeof(recvBuffer), 0,
                                  (struct sockaddr *)&ClientAddr, &clientAddrLen);
    if (bytesReceived == SOCKET_ERROR)
    {
        std::cerr << "ERROR: Failed to receive data" << std::endl;
        closesocket(ServerSocket);
        WSACleanup();
        return 1;
    }

    std::cout << "SUCCESS: Received data" << std::endl;

    // Null-terminate the received string and print it
    recvBuffer[bytesReceived] = '\0';  // Null-terminate the string
    std::cout << "Received from client: " << recvBuffer << std::endl;

    // Send response to client
    const char *response = "Hello from server!";
    int sendResult = sendto(ServerSocket, response, strlen(response), 0,
                            (struct sockaddr *)&ClientAddr, clientAddrLen);
    if (sendResult == SOCKET_ERROR)
    {
        std::cerr << "ERROR: Failed to send data to client" << std::endl;
        closesocket(ServerSocket);
        WSACleanup();
        return 1;
    }

    std::cout << "SUCCESS: Sent data to client" << std::endl;

    // Cleanup
    closesocket(ServerSocket);  // Close the server socket
    WSACleanup();  // Clean up Winsock

    return 0;
}
