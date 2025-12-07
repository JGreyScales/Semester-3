#ifndef DISPLAY_H
#define DISPLAY_H

#include <string>
#include <list>

// Display all posts received from server
void displayAllPosts(const std::list<std::string>& rawPostData);

// Display a single formatted post
void displayPost(const std::string& author, const std::string& topic, const std::string& body, int postNumber);

#endif
