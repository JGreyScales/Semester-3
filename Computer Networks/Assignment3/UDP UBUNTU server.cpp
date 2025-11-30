#include <iostream>
#include <cstring>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

int main()
{
    // Create Socket for UDP
    int ServerSocket = socket(AF_INET, SOCK_DGRAM, 0); // we use SOCK_DGRAM for UDP instead of SOCK_STREAM
    // the stream concept in C is a connection oriented protocol, hence why we need to change it
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

    // Prepare buffer for receiving data
    char recvBuffer[1024];
    sockaddr_in ClientAddr; // we store the client address so we can send data back
    // while this isnt strictly needed, I wanted to extent the task a bit further
    socklen_t clientAddrLen = sizeof(ClientAddr);

    // Receive data from any client (no connection needed for UDP)
    int bytesReceived = recvfrom(ServerSocket, recvBuffer, sizeof(recvBuffer), 0,
                                  (struct sockaddr *)&ClientAddr, &clientAddrLen);
    if (bytesReceived == -1)
    {
        std::cerr << "ERROR: Failed to receive data" << std::endl;
        close(ServerSocket);
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
    if (sendResult == -1)
    {
        std::cerr << "ERROR: Failed to send data to client" << std::endl;
        close(ServerSocket);
        return 1;
    }

    std::cout << "SUCCESS: Sent data to client" << std::endl;

    // Cleanup
    close(ServerSocket);  // Close the server socket

    return 0;
}
