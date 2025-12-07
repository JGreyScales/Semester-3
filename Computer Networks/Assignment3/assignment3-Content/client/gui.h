#ifndef GUI_H
#define GUI_H

#include <string>
#include <list>

struct Post {
    std::string author;
    std::string topic;
    std::string body;
};

// Display menu and get user's choice
int displayMenu();

// Get single post information from user
Post getSinglePostFromUser();

// Get multiple posts from user
std::list<Post> getMultiplePostsFromUser();

// Display a confirmation message
void displayConfirmation(const std::string& message);

#endif
