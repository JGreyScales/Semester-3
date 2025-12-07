#include <iostream>
#include "clientNetwork.h"
#include "gui.h"
#include "display.h"
#include "../commands.h"

using namespace std;

int main()
{
    ClientNetwork client;
    
    // Connect to server
    const char* serverIP = "172.16.5.12";
    int serverPort = 27000;
    
    if (!client.connectToServer(serverIP, serverPort)) {
        return 1;
    }
    
    // Main application loop
    bool running = true;
    while (running) {
        int choice = displayMenu();
        
        switch (choice) {
            case 1: {
                // Submit single post
                Post post = getSinglePostFromUser();
                string command = CONSTRUCT_SUBMIT_SINGLE_POST(post.author, post.topic, post.body);
                
                if (client.sendCommand(command)) {
                    string response = client.receiveResponse();
                    if (response.find("STATUS: success") != string::npos) {
                        displayConfirmation("✓ Post submitted successfully!");
                    } else {
                        displayConfirmation("✗ Failed to submit post.");
                    }
                }
                break;
            }
            
            case 2: {
                // Submit multiple posts
                list<Post> posts = getMultiplePostsFromUser();
                
                string command = BEGIN_MULTIPOST();
                for (const Post& post : posts) {
                    command = ADD_TO_MULTIPOST(post.author, post.topic, post.body, command);
                }
                command = FINALIZE_MULTIPOST(command);
                
                if (client.sendCommand(command)) {
                    string response = client.receiveResponse();
                    if (response.find("STATUS: success") != string::npos) {
                        displayConfirmation("✓ All posts submitted successfully!");
                    } else {
                        displayConfirmation("✗ Failed to submit posts.");
                    }
                }
                break;
            }
            
            case 3: {
                // Get all posts
                string command = CONSTRUCT_GET_ALL_POSTS();
                
                if (client.sendCommand(command)) {
                    list<string> allPosts = client.receiveAllPosts();
                    displayAllPosts(allPosts);
                }
                break;
            }
            
            case 4: {
                // Exit
                cout << "\nDisconnecting from server..." << endl;
                running = false;
                break;
            }
            
            default: {
                cout << "\nInvalid choice. Please enter 1-4." << endl;
                break;
            }
        }
    }
    
    client.disconnect();
    cout << "Goodbye!" << endl;
    
    return 0;
}