#include <winsock2.h>
#include <iostream>
#include <thread>
#include <chrono>

#include "gui.h"
#include "../commands.h"
using namespace std;

// Link the Winsock library
#pragma comment(lib, "ws2_32.lib")

int main()
{
    // Initialize Winsock
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
    {
        cout << "ERROR: Failed to start WSA" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started WSA" << std::endl;

    // Create the socket
    SOCKET ClientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ClientSocket == INVALID_SOCKET)
    {
        WSACleanup();
        cout << "ERROR: Failed to create ClientSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started ClientSocket" << std::endl;

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
        cout << "ERROR: Connection attempt failed " << IP << ':' << port << std::endl;
        return 0;
    }

    cout << "SUCCESS: Successfully connected to the server at " << IP << ':' << port << std::endl;


    std::string result = CONSTRUCT_SUBMIT_SINGLE_POST("bob", "topic", "body");

    const char *c_result = result.c_str();
    int sendResult = send(ClientSocket, c_result, strlen(c_result), 0);
    if (sendResult == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to send data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }

    cout << "Message sent to server: " << c_result << endl;

    char recvBuffer[1024];

    int bytesReceived = recv(ClientSocket, recvBuffer, sizeof(recvBuffer), 0);
    if (bytesReceived == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to receive data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }

    cout << "SUCCESS: Recieved Data" << std::endl;
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    cout << "Received from server: " << recvBuffer << endl;


    std::string result2 = BEGIN_MULTIPOST();
    result2 = ADD_TO_MULTIPOST("author 1", "topic 1", "body 1", result2);
    result2 = ADD_TO_MULTIPOST("author 2", "topic 2", "body 2", result2);
    result2 = ADD_TO_MULTIPOST("author 3", "topic 3", "body 3", result2);
    result2 = FINALIZE_MULTIPOST(result2);

    const char *c_result2 = result2.c_str();
    int sendResult2 = send(ClientSocket, c_result2, strlen(c_result2), 0);
    if (sendResult2 == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to send data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }
    // Cleanup: Close the socket and cleanup Winsock
    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}
