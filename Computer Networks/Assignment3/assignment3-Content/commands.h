#include <string>
#include <string>
#include <iostream>
#include <list>

std::string delimiter = ":";
const int BUFFERSIZE = 1024;

std::string GET_NEXT_ITEM(std::string *command)
{
    size_t chopPoint = command->find(delimiter);
    std::string extractedValue;
    if (chopPoint == std::string::npos)
    {
        extractedValue = *command;
        command->clear();
    }
    else
    {
        extractedValue = command->substr(0, chopPoint);
        *command = command->substr(chopPoint + delimiter.length());
    }
    return extractedValue;
}


// 1 = submit single post
// 2 = submit multipost
// 3 = fetch all posts
// 4 = user auth
// 5 = user signup
int ID_COMMAND(std::string *command)
{

    std::string commandID = GET_NEXT_ITEM(command);

    try
    {
        return std::stoi(commandID);
    }
    catch (const std::exception &e)
    {
        std::cout << e.what() << '\n';
        return 0;
    }
}

std::string CONSTRUCT_SUBMIT_SINGLE_POST(std::string author, std::string topic, std::string body)
{
    std::string command = "1";
    command = command + delimiter + author;
    command = command + delimiter + topic;
    command = command + delimiter + body + '\0';
    return command;
}

std::string BEGIN_MULTIPOST(){
    std::string command = "2";
    return command;
}

std::string ADD_TO_MULTIPOST(std::string author, std::string topic, std::string body, std::string existingCommand){
    existingCommand = existingCommand + delimiter + author;
    existingCommand = existingCommand + delimiter + topic;
    existingCommand = existingCommand + delimiter + body;

    return existingCommand;
}

std::string FINALIZE_MULTIPOST(std::string existingCommand){
    return existingCommand + "\0";
}

std::string CONSTRUCT_GET_ALL_POSTS(){
    return "3\0";
}

std::list<std::string> EXTRACT_ALL_DATA(std::string *command)
{
    std::list<std::string> extractedData;

    while (command->length() > 0){
        std::string nextValue = GET_NEXT_ITEM(command);
        extractedData.insert(extractedData.end(), nextValue);
    }

    return extractedData;
}