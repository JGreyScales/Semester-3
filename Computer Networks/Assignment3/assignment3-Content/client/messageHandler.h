    
#include <winsock2.h>
#include "../commands.h"


bool sendToServer(SOCKET ClientSocket, std::string* message)
{
    const char *c_result = (*message).c_str();
    int sendResult = send(ClientSocket, c_result, strlen(c_result), 0);
    if (sendResult == SOCKET_ERROR)
    {
        std::cout << "ERROR: Failed to send data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return false;
    }
    return true;
}

int recieveFromServer(SOCKET ClientSocket, char* recvBuffer){
    // if we do len of buffer it will be 8, cuz it will return the length of the pointer
    int bytesReceived = recv(ClientSocket, recvBuffer, BUFFERSIZE, 0);
    if (bytesReceived == SOCKET_ERROR)
    {
        std::cout << "ERROR: Failed to receive data" << std::endl;
        closesocket(ClientSocket);
        WSACleanup();
        return 0;
    }
    recvBuffer[bytesReceived] = '\0'; // Null-terminate the string
    return bytesReceived;
}