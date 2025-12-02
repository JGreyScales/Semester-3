#include <string>
#include <list>
#include <fstream>
#include <iostream>
#include <sys/stat.h>

const int BUFFERSIZE = 1024;
const std::string POSTFILE = "posts.txt";

// https://stackoverflow.com/questions/12774207/fastest-way-to-check-if-a-file-exists-using-standard-c-c11-14-17-c
bool postsFileExists()
{
    struct stat buffer;
    bool fileExists = (stat(POSTFILE.c_str(), &buffer) == 0);
    return fileExists;
}

void recoverPostsFile()
{
    std::cout << "Creating storage file" << std::endl;
    std::ofstream file;
    file.open(POSTFILE);
    file.close();
}

bool appendToPosts(std::list<std::string> data)
{
    if (!postsFileExists())
    {
        recoverPostsFile();
    }
    std::ofstream postFILE;

    // open in append
    postFILE.open(POSTFILE, std::ios_base::app);
    if (postFILE.is_open())
    {
        for (std::string value : data)
        {
            postFILE << "\n" << value;
        }
        postFILE.close();
        return true;
    }
    return false;
}

std::list<std::string> readFromPostsFile()
{
    if (!postsFileExists())
    {
        recoverPostsFile();
    }
    std::ifstream postFILE(POSTFILE);
    std::list<std::string> results;
    std::string stream;
    if (postFILE.is_open()){
        while (getline(postFILE, stream)){
            results.insert(results.end(), stream);
        }
        postFILE.close();
    }
    return results;
}

