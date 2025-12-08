#include <iostream>
#include <string>
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

#define A1_PORT 25500    // UDP destination (A1)
#define A2_PORT 29100    // TCP server port (A2)

SOCKET connect_tcp_to_A2(const char* ip, int port) {
    SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET) {
        std::cerr << "TCP socket() failed: " << WSAGetLastError() << std::endl;
        exit(1);
    }

    sockaddr_in addr{};
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    inet_pton(AF_INET, ip, &addr.sin_addr);

    if (connect(sock, (sockaddr*)&addr, sizeof(addr)) == SOCKET_ERROR) {
        std::cerr << "TCP connect() failed: " << WSAGetLastError() << std::endl;
        closesocket(sock);
        exit(1);
    }

    return sock;
}

SOCKET create_udp_sender() {
    SOCKET sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (sock == INVALID_SOCKET) {
        std::cerr << "UDP socket() failed: " << WSAGetLastError() << std::endl;
        exit(1);
    }
    return sock;
}

int main() {
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) {
        std::cerr << "WSAStartup failed\n";
        return 1;
    }

    // REQ_A3_2
    SOCKET a2Sock = connect_tcp_to_A2("127.0.0.1", A2_PORT);

    SOCKET a1Sock = create_udp_sender();
    sockaddr_in a1Addr{};
    a1Addr.sin_family = AF_INET;
    a1Addr.sin_port = htons(A1_PORT);
    inet_pton(AF_INET, "127.0.0.1", &a1Addr.sin_addr);

    while (true) {
        // REQ_A3_3
        std::cout << "Enter message: ";
        std::string msg;
        std::getline(std::cin, msg);

        // REQ_A3_4 & REQ_A3_5 & REQ_A3_8_A
        if (sendto(a1Sock, msg.c_str(), (int)msg.size(), 0,
                   (sockaddr*)&a1Addr, sizeof(a1Addr)) == SOCKET_ERROR) {
            std::cerr << "sendto() failed: " << WSAGetLastError() << std::endl;
            break;
        }

        if (msg == "Done") {
            char buffer[1024];
            // REQ_A3_8_B
            int n = recv(a2Sock, buffer, sizeof(buffer) - 1, 0);
         
            // REQ_A3_8_C
            closesocket(a1Sock);
            closesocket(a2Sock);
            WSACleanup();
            return 0;
        }


        char buffer[1024];
        // REQ_A3_6
        int bytesRecieved = recv(a2Sock, buffer, sizeof(buffer) - 1, 0);
        buffer[bytesRecieved] = '\0';
        std::cout << "Message Received: " << buffer << std::endl;
    }

    closesocket(a1Sock);
    closesocket(a2Sock);
    WSACleanup();
    return 0;
}
