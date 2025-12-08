#include <iostream>
#include <cstring>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

#define A3_PORT 25500
#define A2_PORT 21000
#define A2_ADDR "127.0.0.1"

int main()
{
    int a3Sock = socket(AF_INET, SOCK_DGRAM, 0); // we use SOCK_DGRAM for UDP instead of SOCK_STREAM
    int a2Sock = socket(AF_INET, SOCK_DGRAM, 0);

    sockaddr_in a3Addr;
    a3Addr.sin_family = AF_INET;
    a3Addr.sin_addr.s_addr = INADDR_ANY;
    // REQ_A1_2
    a3Addr.sin_port = htons(A3_PORT);

    sockaddr_in a2Addr;
    a2Addr.sin_family = AF_INET;
    a2Addr.sin_port = htons(A2_PORT);
    a2Addr.sin_addr.s_addr = inet_addr(A2_ADDR);

    bind(a3Sock, (struct sockaddr *)&a3Addr, sizeof(a3Addr));

    char recvBuffer[1024];


    while (1)
    {
        int bytesReceived = recvfrom(a3Sock, recvBuffer, sizeof(recvBuffer), 0, nullptr, nullptr);

        recvBuffer[bytesReceived] = '\0';
        // REQ_A1_3_A
        std::cout << "Message Received: " << recvBuffer << std::endl; 
        // REQ_A1_3_B & REQ_A1_4_A & REQ_A1_5
        sendto(a2Sock, recvBuffer, bytesReceived, 0, (sockaddr*)&a2Addr, sizeof(a2Addr));

        if (strncmp(recvBuffer, "Done", 4) == 0) 
        {
            // REQ_A1_4_B
            close(a3Sock);
            close(a2Sock);
            return 0;
        }

    }
    return 0;
}