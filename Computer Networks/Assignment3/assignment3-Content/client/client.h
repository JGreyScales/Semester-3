#include <thread>
#include <chrono>
#include "clientAuth.h"

// 0 == fine
// 1 == client not authed
std::list<std::string> getMultiPost()
{
    std::list<std::string> posts;
    bool constructing = true;
    char userCommand;

    while (constructing)
    {
        std::string multiPost = BEGIN_MULTIPOST();
        std::string author, topic, body;
        std::cout << "\nEnter author: ";
        std::cin >> author;
        std::cout << "\nEnter topic: ";
        std::cin >> topic;
        std::cout << "\nEnter body: ";
        std::cin >> body;

        if (author.length() + topic.length() + body.length() >= BUFFERSIZE - 4)
        {
            std::cout << "Post is too long" << std::endl;
            continue;
        }

        multiPost = ADD_TO_MULTIPOST(author, topic, body, multiPost);
        multiPost = FINALIZE_MULTIPOST(multiPost);
        posts.push_back(multiPost);
        multiPost.clear();

        std::cout << "\nAdd another post? [Y/N]: ";
        std::cin >> userCommand;
        if (userCommand != 'Y')
            constructing = false;
    }

    std::cout << "Sending " << posts.size() << " packet(s)" << std::endl;
    return posts;
}

std::string getSinglePost()
{
    bool constructing = true;
    std::string author;
    std::string topic;
    std::string body;
    while (constructing)
    {
        std::cout << std::endl
                  << "Enter author: ";
        std::cin >> author;

        std::cout << std::endl
                  << "Enter topic: ";
        std::cin >> topic;

        std::cout << std::endl
                  << "Enter body: ";
        std::cin >> body;

        if (author.length() + topic.length() + body.length() >= BUFFERSIZE - 4)
        {
            std::cout << "post is too long" << std::endl;
            continue;
        }
        constructing = false;
    }
    return CONSTRUCT_SUBMIT_SINGLE_POST(author, topic, body);
}

int clientSession(SOCKET ClientSocket)
{
    char recvBuffer[1024];

    if (!authUser(ClientSocket, recvBuffer))
    {
        std::cout << "Client was not authorized correctly, closing connection" << std::endl;
        return 1;
    }

    bool clientMantainingConnection = true;
    char userCommand;
    int userCommandDigit;

    while (clientMantainingConnection)
    {
        std::cout << "Please select your command:" << std::endl
                  << "    1. Submit post" << std::endl
                  << "    2. Submit multi post" << std::endl
                  << "    3. Fetch all posts" << std::endl
                  << "    4. Exit" << std::endl;
        std::cin >> userCommand;

        userCommandDigit = (int)(userCommand - '0');
        if (userCommandDigit < 1 && userCommandDigit > 4)
        {
            std::cout << "Invalid command entered" << std::endl;
            continue;
        }

        switch (userCommandDigit)
        {
        case 1:
        {
            std::string message = getSinglePost();
            sendToServer(ClientSocket, &message);
            recieveFromServer(ClientSocket, recvBuffer);
            std::cout << recvBuffer << std::endl;
            break;
        }

        case 2:
        {
            std::list<std::string> posts = getMultiPost();
            for (std::string post : posts)
            {
                sendToServer(ClientSocket, &post);
                recieveFromServer(ClientSocket, recvBuffer);
                std::cout << recvBuffer << std::endl;
            }
            break;
        }

        case 3:
        {
            std::string command = CONSTRUCT_GET_ALL_POSTS();
            sendToServer(ClientSocket, &command);
            std::list<std::string> posts;
            bool recieving = true;
            while (recieving)
            {
                if (recieveFromServer(ClientSocket, recvBuffer) == 38)
                {
                    std::cout << "Bits line up for ending" << std::endl;
                    // +1 because [0] is a null value
                    if (strncmp(recvBuffer + 1, "FIN", 3) == 0)
                    {
                        std::cout << "FIN recieved" << std::endl;
                        recieving = false;
                    }
                }
                else
                {
                    std::string recvBufferString = recvBuffer;
                    for (std::string data : EXTRACT_ALL_DATA(&recvBufferString))
                    {
                        posts.push_back(data);
                    }
                    std::cout << posts.size() << std::endl;
                }
            }
            int ticker = 0;
            for (std::string value : posts){
                if (ticker % 3 == 0){
                    std::cout << std::endl;
                    std::cout << "POST " << (ticker / 3) + 1 << ": ";
                } else {
                    std::cout << " " << value;
                }
                ticker++;
            }
            std::cout << std::endl;
            break;
        }

        case 4:
        {
            clientMantainingConnection = false;
            break;
        }

        default:
            break;
        }
    }
    return 0;
}