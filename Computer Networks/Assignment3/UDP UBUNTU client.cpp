#include <iostream>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>

using namespace std;

int main()
{
    // Create the socket
    int clientSocket = socket(AF_INET, SOCK_DGRAM, 0);  // SOCK_DGRAM for UDP
    if (clientSocket < 0) {
        perror("ERROR: Failed to create socket");
        return 1;
    }

    cout << "SUCCESS: Created ClientSocket" << endl;

    // Set up the server address structure
    sockaddr_in serverAddr;
    int port = 27500;
    const char *IP = "10.2.1.10";

    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(port);  // Convert port to network byte order
    serverAddr.sin_addr.s_addr = inet_addr(IP);  // Set IP address

    // Example: Send data to the server
    const char *message = "Hello, Server!";
    int sendResult = sendto(clientSocket, message, strlen(message), 0, 
                            (struct sockaddr *)&serverAddr, sizeof(serverAddr));
    if (sendResult < 0) {
        perror("ERROR: Failed to send data");
        close(clientSocket);
        return 1;
    }

    cout << "Message sent to server: " << message << endl;

    // Receive data from the server
    char recvBuffer[1024];
    socklen_t serverAddrLen = sizeof(serverAddr);
    int bytesReceived = recvfrom(clientSocket, recvBuffer, sizeof(recvBuffer), 0, 
                                  (struct sockaddr *)&serverAddr, &serverAddrLen);
    if (bytesReceived < 0) {
        perror("ERROR: Failed to receive data");
        close(clientSocket);
        return 1;
    }

    cout << "SUCCESS: Received Data" << endl;
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    cout << "Received from server: " << recvBuffer << endl;

    // Cleanup: Close the socket
    close(clientSocket);

    return 0;
}
