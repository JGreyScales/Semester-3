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

    std::ofstream postFILE(POSTFILE, std::ios_base::app);
    if (!postFILE.is_open())
        return false;

    // Check if the file is empty
    bool firstLine = true;
    std::ifstream checkFile(POSTFILE);
    if (checkFile.peek() != std::ifstream::traits_type::eof()) {
        firstLine = false; // file is not empty
    }
    checkFile.close();

    for (std::string value : data)
    {
        if (!firstLine) postFILE << "\n";
        postFILE << value;
        firstLine = false;
    }

    postFILE.close();
    return true;
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

