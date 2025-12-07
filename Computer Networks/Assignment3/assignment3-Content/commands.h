#ifndef COMMANDS_H
#define COMMANDS_H

#include <string>
#include <list>

extern std::string delimiter;

std::string GET_NEXT_ITEM(std::string *command);
int ID_COMMAND(std::string *command);
std::string CONSTRUCT_SUBMIT_SINGLE_POST(std::string author, std::string topic, std::string body);
std::string BEGIN_MULTIPOST();
std::string ADD_TO_MULTIPOST(std::string author, std::string topic, std::string body, std::string existingCommand);
std::string FINALIZE_MULTIPOST(std::string existingCommand);
std::string CONSTRUCT_GET_ALL_POSTS();
std::list<std::string> EXTRACT_ALL_DATA(std::string *command);

#endif