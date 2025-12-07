#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include <string>

// Server settings
const int MOCK_PORT = 27000;
const char* MOCK_IP = "172.16.5.12";
const int BUFFER_SIZE = 1024;

// Predefined responses for client testing
const char* ACK_SUCCESS = "FIN: message received STATUS: success\n";

const char* MOCK_POSTS =
"MockAuthor1:TestTopic1:This is the first mock post body.\n"
"AnonUser::This post has an empty topic field (testing rule 2).\n"
"FIN: message received STATUS: success\n";

// Handles a single client connection
void handle_client_connection(int ConnectionSocket) {
    char recvBuffer[BUFFER_SIZE];
    int bytesReceived;

    // Receive data from client
    bytesReceived = recv(ConnectionSocket, recvBuffer, sizeof(recvBuffer) - 1, 0);
    if (bytesReceived <= 0) {
        close(ConnectionSocket);
        return;
    }

    // Make buffer a valid string
    recvBuffer[bytesReceived] = '\0';
    std::string command = recvBuffer;

    std::cout << "\nReceived from client: " << command << std::endl;

    // First character tells which menu option was used
    char commandID = command[0];

    // Send responses based on menu option
    if (commandID == '1' || commandID == '2') {
        // For submit single or multiple posts
        send(ConnectionSocket, ACK_SUCCESS, strlen(ACK_SUCCESS), 0);
    }
    else if (commandID == '3') {
        // For get-all-posts request
        send(ConnectionSocket, MOCK_POSTS, strlen(MOCK_POSTS), 0);
    }
    else {
        std::cout << "Unknown command received.\n";
    }

    // Close connection after response
    close(ConnectionSocket);
}

int main() {
    std::cout << "Mock Server running on " << MOCK_IP << ":" << MOCK_PORT << "\n";

    // Create server socket
    int ServerSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ServerSocket < 0) return 1;

    // Allow quick restart of server
    int opt = 1;
    setsockopt(ServerSocket, SOL_SOCKET, SO_REUSEADDR, &opt, sizeof(opt));

    // Setup server address structure
    sockaddr_in SvrAddr;
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_addr.s_addr = inet_addr(MOCK_IP);
    SvrAddr.sin_port = htons(MOCK_PORT);

    // Bind socket to IP and port
    if (bind(ServerSocket, (struct sockaddr*)&SvrAddr, sizeof(SvrAddr)) < 0) {
        std::cerr << "Bind failed.\n";
        close(ServerSocket);
        return 1;
    }

    // Start listening for connections
    if (listen(ServerSocket, 5) < 0) {
        std::cerr << "Listen failed.\n";
        close(ServerSocket);
        return 1;
    }

    // Main loop: accept and handle clients one by one
    while (true) {
        std::cout << "Waiting for client...\n";

        int ConnectionSocket = accept(ServerSocket, NULL, NULL);
        if (ConnectionSocket < 0) continue;

        std::cout << "Client connected.\n";
        handle_client_connection(ConnectionSocket);
    }

    // Close server socket (not usually reached)
    close(ServerSocket);
    return 0;
}
