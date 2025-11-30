#include <iostream>
#include <cstring>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

int main()
{
    // Create Socket
    int ServerSocket = socket(AF_INET, SOCK_STREAM, 0);  // Use 0 for IPPROTO_TCP (default)
    if (ServerSocket == -1)
    {
        std::cerr << "ERROR: Failed to create ServerSocket" << std::endl;
        return 1;
    }

    std::cout << "SUCCESS: Started ServerSocket" << std::endl;

    // Bind the socket to a port
    sockaddr_in SvrAddr;
    int port = 27500;
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_addr.s_addr = INADDR_ANY;  // Listen on any available interface
    SvrAddr.sin_port = htons(port);

    if (bind(ServerSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == -1)
    {
        close(ServerSocket);  // Close the socket on error
        std::cerr << "ERROR: Failed to bind ServerSocket" << std::endl;
        return 1;
    }

    std::cout << "SUCCESS: Binded ServerSocket to port " << port << std::endl;

    // Start listening for incoming connections
    if (listen(ServerSocket, 1) == -1)
    {
        close(ServerSocket);  // Close the socket on error
        std::cerr << "ERROR: Listen failed to configure ServerSocket" << std::endl;
        return 1;
    }

    std::cout << "SUCCESS: Listening on ServerSocket" << std::endl;

    // Accept a client connection
    int ConnectionSocket = accept(ServerSocket, NULL, NULL);
    if (ConnectionSocket == -1)
    {
        close(ServerSocket);  // Close the server socket on error
        std::cerr << "ERROR: Failed to accept connection" << std::endl;
        return 1;
    }

    std::cout << "SUCCESS: Accepted connection" << std::endl;

    // Receive data from the client
    char recvBuffer[1024];  // A buffer to store the received data
    int bytesReceived = recv(ConnectionSocket, recvBuffer, sizeof(recvBuffer), 0);
    if (bytesReceived == -1)
    {
        std::cerr << "ERROR: Failed to receive data" << std::endl;
        close(ConnectionSocket);
        close(ServerSocket);
        return 1;
    }

    std::cout << "SUCCESS: Received data" << std::endl;

    // Null-terminate the received string and print it
    recvBuffer[bytesReceived] = '\0';  // Null-terminate the string
    std::cout << "Received from client: " << recvBuffer << std::endl;

    // Send response to client
    const char *response = "Hello from server!";
    int sendResult = send(ConnectionSocket, response, strlen(response), 0);
    if (sendResult == -1)
    {
        std::cerr << "ERROR: Failed to send data to client" << std::endl;
        close(ConnectionSocket);
        close(ServerSocket);
        return 1;
    }

    std::cout << "SUCCESS: Sent data to client" << std::endl;

    // Cleanup
    close(ConnectionSocket);  // Close the client connection socket
    close(ServerSocket);      // Close the server socket

    return 0;
}
