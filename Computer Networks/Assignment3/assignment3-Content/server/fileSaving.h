#include <string>
#include <list>
#include <fstream>
#include <iostream>
#include <sys/stat.h>
#include <mutex>

std::mutex fileLock; // we will use lock_guard so that when it goes out of scope it unlocks on its own
// its one less headache to worry about

// we use this mutex on 2 seperate files, if we want perfect CPU optimization we would split this up
const std::string POSTFILE = "posts.txt";
const std::string USERFILE = "users.txt";

// https://stackoverflow.com/questions/12774207/fastest-way-to-check-if-a-file-exists-using-standard-c-c11-14-17-c
bool fileExists(const std::string* file)
{
    struct stat buffer;
    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);
    bool fileExists = (stat((*file).c_str(), &buffer) == 0);
    return fileExists;
}

void recoverFile(const std::string* file)
{
    std::cout << "Creating storage file: " << *file << std::endl;
    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);
    std::ofstream openedFile(*file);
    openedFile.close();
    // No need to manually unlock, it's done automatically by lock_guard
}

bool appendToFile(std::list<std::string> data, const std::string* file)
{
    std::cout << "appending to " << *file << std::endl;
    if (!fileExists(file))
    {
        recoverFile(file);
    }

    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);

    std::ofstream openedFile(*file, std::ios_base::app);
    if (!openedFile.is_open())
    {
        return false;
    }

    // Check if the file is empty
    bool firstLine = true;
    std::ifstream checkFile(*file);
    if (checkFile.peek() != std::ifstream::traits_type::eof())
    {
        firstLine = false;  // file is not empty
    }
    checkFile.close();

    for (const std::string& value : data)
    {
        if (!firstLine) 
            openedFile << "\n";
        openedFile << value;
        firstLine = false;
    }

    openedFile.close();
    return true;
}

std::list<std::string> readFromFile(const std::string* file)
{
    if (!fileExists(file))
    {
        recoverFile(file);
    }

    // Locking the mutex using lock_guard
    std::lock_guard<std::mutex> lock(fileLock);

    std::ifstream openedFile(*file);
    std::list<std::string> results;
    std::string stream;
    if (openedFile.is_open())
    {
        while (getline(openedFile, stream))
        {
            results.push_back(stream);
        }
        openedFile.close();
    }

    return results;
}
