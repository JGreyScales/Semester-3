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

    // std::string result = CONSTRUCT_SUBMIT_SINGLE_POST("bob", "topic", "body");

    // const char *c_result = result.c_str();
    // int sendResult = send(ClientSocket, c_result, strlen(c_result), 0);
    // if (sendResult == SOCKET_ERROR)
    // {
    //     cout << "ERROR: Failed to send data" << std::endl;
    //     closesocket(ClientSocket);
    //     WSACleanup();
    //     return 0;
    // }

    // cout << "Message sent to server: " << c_result << endl;

    char recvBuffer[1024];

    // int bytesReceived = recv(ClientSocket, recvBuffer, sizeof(recvBuffer), 0);
    // if (bytesReceived == SOCKET_ERROR)
    // {
    //     cout << "ERROR: Failed to receive data" << std::endl;
    //     closesocket(ClientSocket);
    //     WSACleanup();
    //     return 0;
    // }

    // cout << "SUCCESS: Recieved Data" << std::endl;
    // recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    // cout << "Received from server: " << recvBuffer << endl;

    // std::string result2 = BEGIN_MULTIPOST();
    // result2 = ADD_TO_MULTIPOST("author 1", "topic 1", "body 1", result2);
    // result2 = ADD_TO_MULTIPOST("author 2", "topic 2", "body 2", result2);
    // result2 = ADD_TO_MULTIPOST("author 3", "topic 3", "body 3", result2);
    // result2 = FINALIZE_MULTIPOST(result2);

    // const char *c_result2 = result2.c_str();
    // int sendResult2 = send(ClientSocket, c_result2, strlen(c_result2), 0);
    // if (sendResult2 == SOCKET_ERROR)
    // {
    //     cout << "ERROR: Failed to send data" << std::endl;
    //     closesocket(ClientSocket);
    //     WSACleanup();
    //     return 0;
    // }

    // bytesReceived = recv(ClientSocket, recvBuffer, sizeof(recvBuffer), 0);
    // if (bytesReceived == SOCKET_ERROR)
    // {
    //     cout << "ERROR: Failed to receive data" << std::endl;
    //     closesocket(ClientSocket);
    //     WSACleanup();
    //     return 0;
    // }

    // cout << "SUCCESS: Recieved Data" << std::endl;
    // recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    // cout << "Received from server: " << recvBuffer << endl;

    std::string getFromServer = CONSTRUCT_GET_ALL_POSTS();
    const char *c_getFromServer = getFromServer.c_str();
    int sendResult3 = send(ClientSocket, c_getFromServer, strlen(c_getFromServer), 0);
    if (sendResult3 == SOCKET_ERROR)
    {
        cout << "ERROR: Failed to send data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }
    std::string messageBuffer;
    while (true)
    {
        int bytesReceived = recv(ClientSocket, recvBuffer, sizeof(recvBuffer) - 1, 0);
        if (bytesReceived == SOCKET_ERROR || bytesReceived == 0)
        {
            cout << "ERROR or connection closed" << std::endl;
            closesocket(ClientSocket);
            WSACleanup();
            return 0;
        }

        recvBuffer[bytesReceived] = '\0';
        messageBuffer += recvBuffer;

        // tcp does not perserve message boundries, this means that if the final packet
        // of data is less than size of recvBuffer, it will merge the FIN and data into 1 packet
        // we need to manually seperate them using a delimter on the client side to fix this
        while(messageBuffer.length() > 0){
            size_t chopPoint = messageBuffer.find('\n');
            std::string extractedMessage;
            if (chopPoint == std::string::npos){
                extractedMessage = messageBuffer;
                messageBuffer.clear();
            } else {
                extractedMessage = messageBuffer.substr(0, chopPoint);
                messageBuffer = messageBuffer.substr(chopPoint + 1);
            }
            if (extractedMessage.length() > 0){
                cout << "Received message: " << extractedMessage << std::endl;
            }
        }
    }

    // Cleanup: Close the socket and cleanup Winsock
    closesocket(ClientSocket);
    WSACleanup();

    return 0;
}