#include "clientNetwork.h"
#include <iostream>

ClientNetwork::ClientNetwork() : clientSocket(-1), connected(false) {}

// distructor to clean up socket
ClientNetwork::~ClientNetwork() {
    disconnect();
}

bool ClientNetwork::connectToServer(const char* ip, int port) {
    // Create socket
    clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (clientSocket < 0) {
        std::cout << "ERROR: Failed to create ClientSocket" << std::endl;
        return false;
    }
    std::cout << "SUCCESS: Created ClientSocket" << std::endl;
    
    // Setup server address structure
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(port);
    serverAddr.sin_addr.s_addr = inet_addr(ip);
    
    // Connect
    if (connect(clientSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) < 0) {
        std::cout << "ERROR: Connection attempt failed to " << ip << ":" << port << std::endl;
        close(clientSocket);
        return false;
    }
    
    std::cout << "SUCCESS: Connected to server at " << ip << ":" << port << std::endl;
    connected = true;
    return true;
}

bool ClientNetwork::sendCommand(const std::string& command) {
    if (!connected) {
        std::cout << "ERROR: Not connected to server" << std::endl;
        return false;
    }
    
    const char* cCommand = command.c_str();
    int sendResult = send(clientSocket, cCommand, strlen(cCommand), 0);
    
    if (sendResult < 0) {
        std::cout << "ERROR: Failed to send data" << std::endl;
        return false;
    }
    
    std::cout << "SUCCESS: Command sent to server" << std::endl;
    return true;
}

std::string ClientNetwork::receiveResponse() {
    if (!connected) {
        return "";
    }
    
    char recvBuffer[1024];
    int bytesReceived = recv(clientSocket, recvBuffer, sizeof(recvBuffer) - 1, 0);
    
    if (bytesReceived < 0 || bytesReceived == 0) {
        std::cout << "ERROR: Failed to receive data or connection closed" << std::endl;
        return "";
    }
    
    recvBuffer[bytesReceived] = '\0';
    std::cout << "SUCCESS: Received response from server" << std::endl;
    return std::string(recvBuffer);
}

std::list<std::string> ClientNetwork::receiveAllPosts() {
    std::list<std::string> posts;
    std::string messageBuffer;
    char recvBuffer[1024];
    
    while (true) {
        int bytesReceived = recv(clientSocket, recvBuffer, sizeof(recvBuffer) - 1, 0);
        
        if (bytesReceived < 0 || bytesReceived == 0) {
            std::cout << "ERROR: Connection closed while receiving posts" << std::endl;
            break;
        }
        
        recvBuffer[bytesReceived] = '\0';
        messageBuffer += recvBuffer;
        
        // Process all complete messages in the buffer
        while (messageBuffer.length() > 0) {
            size_t chopPoint = messageBuffer.find('\n');
            std::string extractedMessage;
            
            if (chopPoint == std::string::npos) {
                // No newline found, need more data
                extractedMessage = messageBuffer;
                messageBuffer.clear();
            } else {
                // Found newline, extract message
                extractedMessage = messageBuffer.substr(0, chopPoint);
                messageBuffer = messageBuffer.substr(chopPoint + 1);
            }
            
            if (extractedMessage.length() > 0) {
                // Check if this is the final message
                if (extractedMessage.find("FIN:") != std::string::npos) {
                    std::cout << "SUCCESS: Received all posts from server" << std::endl;
                    return posts;
                }
                
                // Otherwise, it's post data
                posts.push_back(extractedMessage);
            }
        }
    }
    
    return posts;
}

void ClientNetwork::disconnect() {
    if (connected) {
        close(clientSocket);
        connected = false;
        std::cout << "SUCCESS: Disconnected from server" << std::endl;
    }
}

bool ClientNetwork::isConnected() const {
    return connected;
}
