#include "gui.h"
#include <iostream>
#include <limits>

int displayMenu() {
    std::cout << "\n========================================" << std::endl;
    std::cout << "       SOCIAL MEDIA CLIENT MENU" << std::endl;
    std::cout << "========================================" << std::endl;
    std::cout << "1. Submit a Single Post" << std::endl;
    std::cout << "2. Submit Multiple Posts" << std::endl;
    std::cout << "3. Get All Posts from Server" << std::endl;
    std::cout << "4. Exit" << std::endl;
    std::cout << "========================================" << std::endl;
    std::cout << "Enter your choice (1-4): ";
    
    int choice;
    std::cin >> choice;
    
    // Clear input buffer
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    
    return choice;
}

Post getSinglePostFromUser() {
    Post post;
    
    std::cout << "\n--- Enter Post Information ---" << std::endl;
    
    std::cout << "Author: ";
    std::getline(std::cin, post.author);
    
    std::cout << "Topic: ";
    std::getline(std::cin, post.topic);
    
    std::cout << "Body: ";
    std::getline(std::cin, post.body);
    
    return post;
}

std::list<Post> getMultiplePostsFromUser() {
    std::list<Post> posts;
    
    std::cout << "\nHow many posts would you like to submit? ";
    int count;
    std::cin >> count;
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    
    for (int i = 0; i < count; i++) {
        std::cout << "\n--- Post " << (i + 1) << " of " << count << " ---" << std::endl;
        Post post = getSinglePostFromUser();
        posts.push_back(post);
    }
    
    return posts;
}

void displayConfirmation(const std::string& message) {
    std::cout << "\n" << message << std::endl;
}
