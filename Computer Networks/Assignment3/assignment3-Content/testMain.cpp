#include "commands.h"

int main(int argc, char* argv[])
{
    std::cout << "-- SINGLEPOST --" << std::endl;

    std::string singlePostCommand = CONSTRUCT_SUBMIT_SINGLE_POST("Example author", "Example topic", "Example body");
    int singlePostCommandID = ID_COMMAND(&singlePostCommand);
    std::list<std::string> singlePostCommandData = EXTRACT_ALL_DATA(&singlePostCommand);

    for (const auto &item : singlePostCommandData)
    {
        std::cout << item << std::endl;
    }

    std::cout << "-- MULTIPOST --" << std::endl;

    std::string multiPostCommand = BEGIN_MULTIPOST();
    multiPostCommand = ADD_TO_MULTIPOST("author 1", "topic 1", "body 1", multiPostCommand);
    multiPostCommand = ADD_TO_MULTIPOST("", "topic 2", "body 2", multiPostCommand); // example with no author
    multiPostCommand = FINALIZE_MULTIPOST(multiPostCommand);

    int multiPostCommandID = ID_COMMAND(&multiPostCommand);
    std::list<std::string> multiPostCommandData = EXTRACT_ALL_DATA(&multiPostCommand);

    for (const auto &item : multiPostCommandData)
    {
        std::cout << item << std::endl;
    }

    return 0;
}