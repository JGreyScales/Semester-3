#include "display.h"
#include "../commands.h"
#include <iostream>

void displayPost(const std::string& author, const std::string& topic, const std::string& body, int postNumber) {
    std::cout << "\n----------------------------------------" << std::endl;
    std::cout << "Post #" << postNumber << std::endl;
    std::cout << "----------------------------------------" << std::endl;
    std::cout << "Author: " << author << std::endl;
    std::cout << "Topic:  " << topic << std::endl;
    std::cout << "Body:   " << body << std::endl;
    std::cout << "----------------------------------------" << std::endl;
}

void displayAllPosts(const std::list<std::string>& rawPostData) {
    if (rawPostData.empty()) {
        std::cout << "\nNo posts available on the server." << std::endl;
        return;
    }
    
    std::cout << "\n=======================================" << std::endl;
    std::cout << "       ALL POSTS FROM SERVER" << std::endl;
    std::cout << "=======================================" << std::endl;
    
    int postNumber = 1;
    std::string author, topic, body;
    int fieldCount = 0;
    
    // Parse the colon-delimited data
    for (const std::string& packet : rawPostData) {
        std::string packetCopy = packet;
        std::list<std::string> fields = EXTRACT_ALL_DATA(&packetCopy);
        
        for (const std::string& field : fields) {
            if (fieldCount % 3 == 0) {
                author = field;
            } else if (fieldCount % 3 == 1) {
                topic = field;
            } else {
                body = field;
                // We have a complete post (author, topic, body)
                displayPost(author, topic, body, postNumber);
                postNumber++;
            }
            fieldCount++;
        }
    }
    
    std::cout << "\nTotal posts displayed: " << (postNumber - 1) << std::endl;
}
