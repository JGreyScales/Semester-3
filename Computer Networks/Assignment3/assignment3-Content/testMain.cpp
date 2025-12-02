#include "commands.h"

int main(int argc, char* argv[])
{
    std::cout << "-- SINGLEPOST --" << std::endl;

    std::string singlePostCommand = CONSTRUCT_SUBMIT_SINGLE_POST("Example author", "Example topic", "Example body");
    int singlePostCommandID = ID_COMMAND(&singlePostCommand);
    std::list<std::string> singlePostCommandData = EXTRACT_ALL_DATA(&singlePostCommand);
    std::cout << "commandID: " <<  singlePostCommandID << std::endl;

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
    std::cout << "commandID: " <<  multiPostCommandID << std::endl;

    for (const auto &item : multiPostCommandData)
    {
        std::cout << item << std::endl;
    }


    std::cout << "-- GET ALL POSTS (wont return anything) --" << std::endl;
    std::string getAllPostsCommand = CONSTRUCT_GET_ALL_POSTS();

    std::cout << "commandID: " <<  ID_COMMAND(&getAllPostsCommand) << std::endl;

    std::cout << "-- MISC --" << std::endl;

    // how to convert these commands to a const char *
    std::string result = CONSTRUCT_SUBMIT_SINGLE_POST("x", "y", "z");
    const char* c_result = result.c_str();
    std::cout << "Result as const char*: " << c_result << std::endl;
    return 0;
}