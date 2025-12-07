#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include <string>
#include <list>

// Target server to test
const char* SERVER_IP = "172.16.5.12";
const int SERVER_PORT = 27000;
const int BUFFER_SIZE = 1024;

// Test commands for the server
const char* CMD_SINGLE_POST = "1:ServerTeamAuth:MockTopic:This is a single post to test SVR1.\0";
const char* CMD_MULTI_POST = "2:PostOne:Topic1:BodyA:PostTwo::BodyB\0";
const char* CMD_GET_ALL = "3\0";

// Connect to the real server (returns socket or -1)
int connect_to_server() {
    int clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (clientSocket < 0) return -1;

    sockaddr_in serverAddr{};
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(SERVER_PORT);
    serverAddr.sin_addr.s_addr = inet_addr(SERVER_IP);

    // Try connecting
    if (connect(clientSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) < 0) {
        std::cerr << "ERROR: Could not connect to server.\n";
        close(clientSocket);
        return -1;
    }
    return clientSocket;
}

// Send command and read basic response
bool send_and_receive_ack(int clientSocket, const char* command) {
    // Send the command to server
    if (send(clientSocket, command, strlen(command) + 1, 0) < 0) return false;

    char buffer[BUFFER_SIZE];
    int received = recv(clientSocket, buffer, BUFFER_SIZE - 1, 0);

    // If reply received
    if (received > 0) {
        buffer[received] = '\0';
        std::string response = buffer;
        std::cout << "Server Response: " << response;

        // Look for success message
        return response.find("STATUS: success") != std::string::npos;
    }
    return false;
}

// Handle the multi-line response for "get all posts"
bool receive_all_posts_test(int clientSocket)
{
    // Send the request to server
    if (send(clientSocket, CMD_GET_ALL, strlen(CMD_GET_ALL) + 1, 0) < 0) return false;

    std::string bufferAccum;
    char recvBuffer[BUFFER_SIZE];
    std::list<std::string> lines;

    while (true) {
        // Read part of server output
        int received = recv(clientSocket, recvBuffer, sizeof(recvBuffer) - 1, 0);
        if (received <= 0) return false;

        recvBuffer[received] = '\0';
        bufferAccum += recvBuffer;

        // Split by newline into lines
        size_t pos = bufferAccum.find('\n');
        while (pos != std::string::npos) {
            lines.push_back(bufferAccum.substr(0, pos));
            bufferAccum.erase(0, pos + 1);
            pos = bufferAccum.find('\n');
        }

        // Check for final FIN line
        if (bufferAccum.find("FIN:") != std::string::npos) {
            std::cout << "Lines received: " << lines.size() << "\n";
            return bufferAccum.find("STATUS: success") != std::string::npos;
        }
    }
}

int main() {
    std::cout << "Testing Real Server at " << SERVER_IP << ":" << SERVER_PORT << "\n";

    // Test 1: Single post
    int s1 = connect_to_server();
    if (s1 != -1) {
        send_and_receive_ack(s1, CMD_SINGLE_POST);
        close(s1);
    }

    // Test 2: Multiple posts
    int s2 = connect_to_server();
    if (s2 != -1) {
        send_and_receive_ack(s2, CMD_MULTI_POST);
        close(s2);
    }

    // Test 3: Get all posts
    int s3 = connect_to_server();
    if (s3 != -1) {
        receive_all_posts_test(s3);
        close(s3);
    }

    std::cout << "All tests finished.\n";
    return 0;
}
