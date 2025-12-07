#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include "client.h"
using namespace std;

int main()
{
    cout << "SUCCESS: Starting server" << endl;

    // Socket
    int ServerSocket;
    ServerSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ServerSocket < 0)
    {
        cout << "ERROR: Failed to create ServerSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Started ServerSocket" << std::endl;

    // Set socket options to allow address reuse
    int opt = 1;
    if (setsockopt(ServerSocket, SOL_SOCKET, SO_REUSEADDR, &opt, sizeof(opt)) < 0)
    {
        cout << "WARNING: Failed to set SO_REUSEADDR" << std::endl;
    }

    // Bind
    sockaddr_in SvrAddr;
    int port = 27000;
    SvrAddr.sin_family = AF_INET;
    SvrAddr.sin_addr.s_addr = INADDR_ANY;
    SvrAddr.sin_port = htons(port);
    if (bind(ServerSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) < 0)
    {
        close(ServerSocket); // Close the socket on error
        cout << "ERROR: Failed to bind ServerSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Binded ServerSocket to port " << port << std::endl;

    // Listen
    if (listen(ServerSocket, 1) < 0)
    {
        close(ServerSocket); // Close the socket on error
        cout << "ERROR: Listen failed to configure ServerSocket" << std::endl;
        return 0;
    }

    cout << "SUCCESS: Listening on ServerSocket" << std::endl;
    bool serverShutdown = false;
    int clientIDTicker = 0;
    while (!serverShutdown)
    {

        // Accept
        int ConnectionSocket;
        ConnectionSocket = accept(ServerSocket, NULL, NULL);
        if (ConnectionSocket < 0)
        {
            close(ServerSocket); // Close the server socket on error
            cout << "ERROR: Failed to accept connection" << std::endl;
            return 0;
        }
        clientIDTicker++;

        /*
            The Server application explicitly acknowledges the base requirement SVR6 ("NO THREADS") but assumes that implementing
             multithreading (ADV1) is the intended choice for advanced credit.
            Therefore, the Server is programmed to launch a new thread for every client connection it accepts,
            treating the concurrent access logic as a fundamental and verified part of its architecture.
        */
        std::thread clientThread(clientHandler, &serverShutdown, ConnectionSocket, clientIDTicker);
        // need to dettach the thread to allow this line to run again without error
        clientThread.detach();
    }
    // Cleanup
    // tell all threads to shut down on next pass
    serverShutdown = true;
    close(ServerSocket);

    return 0;
}
