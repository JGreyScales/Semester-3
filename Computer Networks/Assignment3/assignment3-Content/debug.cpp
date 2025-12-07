#include <string>
#include <iostream>
int main(){
    std::string s = "FINS";
    std::string firstThree = s.substr(0, 3);
    std::cout << firstThree;
    return 0;
}