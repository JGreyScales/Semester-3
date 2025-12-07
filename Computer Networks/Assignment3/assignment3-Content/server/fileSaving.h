#include <string>
#include <list>
#include <fstream>
#include <iostream>
#include <sys/stat.h>
#include <mutex>

std::mutex fileLock; // we will use lock_guard so that when it goes out of scope it unlocks on its own
// its one less headache to worry about
const int BUFFERSIZE = 1024;
const std::string POSTFILE = "posts.txt";
const std::string delimiter = ":";

// https://stackoverflow.com/questions/12774207/fastest-way-to-check-if-a-file-exists-using-standard-c-c11-14-17-c
bool postsFileExists()
{
    struct stat buffer;
    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);
    bool fileExists = (stat(POSTFILE.c_str(), &buffer) == 0);
    return fileExists;
}

void recoverPostsFile()
{
    std::cout << "Creating storage file" << std::endl;
    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);
    std::ofstream file(POSTFILE);
    // No need to manually unlock, it's done automatically by lock_guard
}

bool appendToPosts(std::list<std::string> data)
{
    std::cout << "appending to file" << std::endl;
    if (!postsFileExists())
    {
        recoverPostsFile();
    }

    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);

    std::ofstream postFILE(POSTFILE, std::ios_base::app);
    if (!postFILE.is_open())
    {
        return false;
    }

    // Check if the file is empty
    bool firstLine = true;
    std::ifstream checkFile(POSTFILE);
    if (checkFile.peek() != std::ifstream::traits_type::eof())
    {
        firstLine = false;  // file is not empty
    }
    checkFile.close();

    for (const std::string& value : data)
    {
        if (!firstLine) 
            postFILE << "\n";
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

    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);

    std::ifstream postFILE(POSTFILE);
    std::list<std::string> results;
    std::string stream;
    if (postFILE.is_open())
    {
        while (getline(postFILE, stream))
        {
            results.push_back(stream);
        }
        postFILE.close();
    }

    return results;
}
