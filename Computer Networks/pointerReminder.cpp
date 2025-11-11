#include <iostream> 
using namespace std;
 
int main() { 
    int var1 = 3; 
    int *var2 = &var1; 
    int **var3 = &var2;

    cout << "var1: " << "Addr: " << &var1 << " val: " << var1 << endl; 

    cout << "var2: " << "Addr: " << &var2 << " val: " << var2 << " derefs: " << *var2 << endl; 

    cout << "var3: " << "Addr: " << &var3 << " val: " << var3 << " derefs: " << *var3 << " dederefs: " << **var3 << endl; 
    return 0; 
}