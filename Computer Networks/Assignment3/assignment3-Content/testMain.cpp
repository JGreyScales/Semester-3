#include "commands.h"

int main(int argc, char* argv[])
{
    std::string command = CONSTRUCT_SUBMIT_SINGLE_POST("Example author", "Example topic", "Example body");
    int commandID = ID_COMMAND(&command);
    std::list<std::string> data = EXTRACT_ALL_DATA(&command);

    for (const auto &item : data)
    {
        std::cout << item << std::endl;
    }

    return 0;
}