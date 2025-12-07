#ifndef CLIENT_NETWORK_H
#define CLIENT_NETWORK_H

#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include <string>
#include <list>

class ClientNetwork {
private:
    int clientSocket;
    bool connected;
    const int BUFFER_SIZE = 1024;

public:
    ClientNetwork();
    ~ClientNetwork();
    
    // Connect to server
    bool connectToServer(const char* ip, int port);
    
    // Send a command to server
    bool sendCommand(const std::string& command);
    
    // Receive simple response (for single/multi post acknowledgments)
    std::string receiveResponse();
    
    // Receive multiple packets (for GET_ALL_POSTS)
    std::list<std::string> receiveAllPosts();
    
    // Disconnect and cleanup
    void disconnect();
    
    // Check if connected
    bool isConnected() const;
};

#endif
