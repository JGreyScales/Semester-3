#include <iostream>
#include <string>
#include <cstring>
#include <unistd.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <fcntl.h>

#define UDP_PORT 21000
#define TCP_PORT 29100

int create_udp_socket()
{
    int a3Sock = socket(AF_INET, SOCK_DGRAM, 0);
    if (a3Sock < 0)
    {
        perror("UDP socket");
        exit(1);
    }

    sockaddr_in addr;
    addr.sin_family = AF_INET;
    addr.sin_port = htons(UDP_PORT);
    addr.sin_addr.s_addr = inet_addr("127.0.0.1");

    if (bind(a3Sock, (sockaddr *)&addr, sizeof(addr)) < 0)
    {
        perror("UDP bind");
        exit(1);
    }

    return a3Sock;
}

int create_tcp_listening_socket()
{
    int tcpSock = socket(AF_INET, SOCK_STREAM, 0);
    if (tcpSock < 0)
    {
        perror("TCP socket");
        exit(1);
    }

    int enable = 1;
    setsockopt(tcpSock, SOL_SOCKET, SO_REUSEADDR, &enable, sizeof(enable));

    sockaddr_in addr;
    addr.sin_family = AF_INET;
    addr.sin_port = htons(TCP_PORT);
    addr.sin_addr.s_addr = INADDR_ANY;

    if (bind(tcpSock, (sockaddr *)&addr, sizeof(addr)) < 0)
    {
        perror("TCP bind");
        exit(1);
    }

    if (listen(tcpSock, 1) < 0)
    {
        perror("TCP listen");
        exit(1);
    }

    return tcpSock;
}

int main()
{
    // REQ_A2_2_A
    int a3Sock = create_udp_socket();
    // REQ_A2_2_B
    int a2Sock = create_tcp_listening_socket();
    int tcpConnSock = -1;

    while (true)
    {
        // allowing multiple listens & sends to occur at once using file descriptors
        fd_set readfds;
        FD_ZERO(&readfds);

        FD_SET(a3Sock, &readfds);
        FD_SET(a2Sock, &readfds);
        int maxfd = std::max(a3Sock, a2Sock);

        if (tcpConnSock > 0)
        {
            FD_SET(tcpConnSock, &readfds);
            maxfd = std::max(maxfd, tcpConnSock);
        }

        // REQ_A2_3
        if (tcpConnSock < 0)
        {
            std::cout << "Waiting for TCP Connection" << std::endl;
        }

        int activity = select(maxfd + 1, &readfds, nullptr, nullptr, nullptr);

        if (FD_ISSET(a2Sock, &readfds))
        {
            sockaddr_in clientAddr{};
            socklen_t len = sizeof(clientAddr);
            tcpConnSock = accept(a2Sock, (sockaddr *)&clientAddr, &len);
        }

        if (FD_ISSET(a3Sock, &readfds))
        {
            char buffer[1024];
            sockaddr_in srcAddr{};
            socklen_t srcLen = sizeof(srcAddr);

            int n = recvfrom(a3Sock, buffer, sizeof(buffer) - 1, 0,
                             (sockaddr *)&srcAddr, &srcLen);
            buffer[n] = '\0';
            std::string msg(buffer);

            // REQ_A2_4_A
            std::cout << "Message Received: " << msg << std::endl;

            // REQ_A2_4_B & REQ_A2_5_A
            send(tcpConnSock, msg.c_str(), msg.size(), 0);

            if (msg == "Done\n")
            {
                // REQ_A2_5_B
                close(a2Sock);
                close(a3Sock);
                // REQ_A2_5_C
                a2Sock = create_tcp_listening_socket();
                a3Sock = create_udp_socket();
            }
        }

        if (tcpConnSock > 0 && FD_ISSET(tcpConnSock, &readfds))
        {
            char tmp[1];
            int r = recv(tcpConnSock, tmp, 1, 0);
            if (r <= 0)
            {
                close(tcpConnSock);
                tcpConnSock = -1;
            }
        }
    }

    return 0;
}
