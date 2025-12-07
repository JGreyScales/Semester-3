#ifndef COMMANDS_H
#define COMMANDS_H

#include <string>
#include <list>
#include <sstream>

// Parse command ID from string
// Returns: 1 for SUBMIT_SINGLE_POST, 2 for SUBMIT_MULTI_POST, 3 for GET_ALL_POSTS
// Format: "commandID:data1:data2:data3"
int ID_COMMAND(std::string* commandString)
{
    // Check for text-based commands first
    if (commandString->find("SUBMIT_SINGLE_POST") != std::string::npos)
        return 1;
    if (commandString->find("SUBMIT_MULTI_POST") != std::string::npos)
        return 2;
    if (commandString->find("GET_ALL_POSTS") != std::string::npos)
        return 3;
    
    // Check for numeric command ID at the start (format: "1:data" or "2:data" or "3:data")
    if (!commandString->empty() && commandString->at(0) >= '1' && commandString->at(0) <= '3')
    {
        if (commandString->length() == 1 || commandString->at(1) == ':')
        {
            return commandString->at(0) - '0'; // Convert char to int
        }
    }
    
    return 0; // Unknown command
}

// Extract all data fields from command string (delimiter is ":")
// Format: "commandID:data1:data2:data3"
std::list<std::string> EXTRACT_ALL_DATA(std::string* commandString)
{
    std::list<std::string> result;
    std::string delimiter = ":";
    
    // Find the first delimiter (skip command ID)
    size_t pos = commandString->find(delimiter);
    if (pos == std::string::npos)
        return result;
    
    // Skip the command part and start extracting data
    std::string data = commandString->substr(pos + delimiter.length());
    
    // Split by delimiter
    size_t start = 0;
    size_t end = data.find(delimiter);
    
    while (end != std::string::npos)
    {
        std::string token = data.substr(start, end - start);
        result.push_back(token); // Include empty tokens
        start = end + delimiter.length();
        end = data.find(delimiter, start);
    }
    
    // Add the last token
    if (start <= data.length())
    {
        std::string token = data.substr(start);
        result.push_back(token);
    }
    
    return result;
}

#endif // COMMANDS_H
