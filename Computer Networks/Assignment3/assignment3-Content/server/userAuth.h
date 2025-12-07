#include "fileSaving.h"
#include "../commands.h"
#include <algorithm>

bool userSignInSuccess(std::string combo){
    std::list<std::string> users = readFromFile(&USERFILE);
    
    // https://en.cppreference.com/w/cpp/algorithm/find.html
    std::list<std::string>::iterator it = std::find_if(users.begin(), users.end(),
                [&combo](const std::string& line) {
                    return line == combo;
                });

    return it != users.end();
}

bool userSignUp(std::string* combo){
    std::list<std::string> fullCombo;

    fullCombo.push_back(*combo);

    return appendToFile(fullCombo, &USERFILE);
}

// null on error
std::string extractUserPassCombo(std::string* buffer){
    std::list<std::string> data = EXTRACT_ALL_DATA(buffer);
    if (data.size() >= 2) {
        std::list<std::string>::iterator it = data.begin();
        std::string first = *it;
        ++it;  // move to second element
        std::string second = *it;
        // fetch element 2, fetch element 3
        // merge them with a : between
        // the ++ works because it is a std::list<std::string> type
        // therefore it will increment by 1 std::string
        std::string combined = first + ':' + second;
        return combined;
    }
    std::cout << "Data is not long enough, improper message" << std::endl;
    return "";
}