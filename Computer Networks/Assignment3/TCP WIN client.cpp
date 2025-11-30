#include <winsock2.h>
#include <iostream>
using namespace std;

// Link the Winsock library
#pragma comment(lib, "ws2_32.lib")

int main()
{
    // Initialize Winsock
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        cout << "ERROR: Failed to start WSA" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started WSA" << std::endl;

    // Create the socket
    SOCKET ClientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ClientSocket == INVALID_SOCKET) {
        WSACleanup();
        cout << "ERROR: Failed to create ClientSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started ClientSocket" << std::endl;

    // Set up the server address structure
    sockaddr_in SvrAddr;
    int port = 27000;
    const char * IP = "127.0.0.1";
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_port = htons(port); // Convert port to network byte order
    SvrAddr.sin_addr.s_addr = inet_addr(IP); // Localhost address

    // Attempt to connect to the server
    if (connect(ClientSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == SOCKET_ERROR) {
        closesocket(ClientSocket);
        WSACleanup();
        cout << "ERROR: Connection attempt failed " << IP << ':' << port << std::endl;
        return 0;
    }

    cout << "SUCCESS: Successfully connected to the server at " << IP << ':' << port << std::endl;

    // Example: Send data to the server (optional)
    const char *message = "Hello, Server!";
    int sendResult = send(ClientSocket, message, strlen(message), 0);
    if (sendResult == SOCKET_ERROR) {
        cout << "ERROR: Failed to send data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }

    cout << "Message sent to server: " << message << endl;

    char recvBuffer[1024];

    int bytesReceived = recv(ClientSocket, recvBuffer, sizeof(recvBuffer), 0);
    if (bytesReceived == SOCKET_ERROR){
        cout << "ERROR: Failed to receive data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }

    cout << "SUCCESS: Recieved Data" << std::endl;
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    cout << "Received from server: " << recvBuffer << endl;
    // Cleanup: Close the socket and cleanup Winsock
    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}
