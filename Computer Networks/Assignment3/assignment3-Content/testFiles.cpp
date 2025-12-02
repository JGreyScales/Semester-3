#include "server/fileSaving.h"

int main(){
    std::list<std::string> results = readFromPostsFile();
    for (std::string value : results){
        std::cout << value << std::endl;
    }

    appendToPosts(results);
    return 0;
}