#include "messageHandler.h"

bool authUser(SOCKET ClientSocket, char* recvBuffer){
    bool userIsAuthed = false;
    while (!userIsAuthed){
        std::string username = "";
        std::string password = "";
        char command;
        int commandDigit;

        std::cout << std::endl << "Enter your username: ";
        std::cin >> username;

        std::cout << std::endl << "Enter your password: ";
        std::cin >> password;

        if (username.length() + password.length() <= 2){
            std::cout << "Username password combo is too short, must be at least 10 characters in total" << std::endl;
            continue;
        }

        std::cout << std::endl << "Enter command: " << std::endl << "   1: sign in" << std::endl << "   2: sign up" << std::endl;
        std::cin >> command;
        commandDigit = (int)(command - '0') + 3;
        if (commandDigit < 1 && commandDigit > 2){
               std::cout << "Invalid command entered" << std::endl;
            continue;         
        }

        
        std::string fullCommand = std::to_string(commandDigit) + ":" + username + ":" + password;
        if (fullCommand.length() >= BUFFERSIZE){
            std::cout << "username password combo too long" << std::endl;
            continue;
        }  


        if (!sendToServer(ClientSocket, &fullCommand)){
            // error message gets thrown inside the sendToServer function
            continue;
        }

        int bytesRecieved = recieveFromServer(ClientSocket, recvBuffer);
        userIsAuthed = (recvBuffer[bytesRecieved - 1] == '1');
        
        if (!userIsAuthed){
            std::cout << "Invalid combo provided, server denied login" << std::endl;
        }
    }
    return userIsAuthed;
}