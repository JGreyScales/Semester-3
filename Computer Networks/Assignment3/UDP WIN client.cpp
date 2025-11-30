#include <iostream>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <cstring>

using namespace std;

int main()
{
    // Initialize Winsock
    WSADATA wsaData;
    int wsaInit = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (wsaInit != 0) {
        cerr << "ERROR: WSAStartup failed with error code " << wsaInit << endl;
        return 1;
    }

    cout << "SUCCESS: WSA Startup" << endl;

    // Create the socket
    int clientSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);  // SOCK_DGRAM for UDP
    if (clientSocket == INVALID_SOCKET) {
        cerr << "ERROR: Failed to create socket" << endl;
        WSACleanup();
        return 1;
    }

    cout << "SUCCESS: Created ClientSocket" << endl;

    // Set up the server address structure
    sockaddr_in serverAddr;
    int port = 27500;
    const char *IP = "127.0.0.1"; // Localhost address

    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(port);  // Convert port to network byte order
    serverAddr.sin_addr.s_addr = inet_addr(IP);  // Set IP address

    // Example: Send data to the server
    const char *message = "Hello, Server!";
    int sendResult = sendto(clientSocket, message, strlen(message), 0, 
                            (struct sockaddr *)&serverAddr, sizeof(serverAddr));
    if (sendResult == SOCKET_ERROR) {
        cerr << "ERROR: Failed to send data" << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }

    cout << "Message sent to server: " << message << endl;

    // Receive data from the server
    char recvBuffer[1024];
    int serverAddrLen = sizeof(serverAddr);
    int bytesReceived = recvfrom(clientSocket, recvBuffer, sizeof(recvBuffer), 0, 
                                  (struct sockaddr *)&serverAddr, &serverAddrLen);
    if (bytesReceived == SOCKET_ERROR) {
        cerr << "ERROR: Failed to receive data" << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }

    cout << "SUCCESS: Received Data" << endl;
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    cout << "Received from server: " << recvBuffer << endl;

    // Cleanup: Close the socket and clean up Winsock
    closesocket(clientSocket);
    WSACleanup();

    return 0;
}
