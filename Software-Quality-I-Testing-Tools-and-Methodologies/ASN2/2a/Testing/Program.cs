using System;

class Program
{
    static void Login(string user, string password, int attempts)
    {
        if (user == "admin") 
        {
            if (password == "1234") 
            {
                Console.WriteLine("Login successful");
            }
            else
            {
                if (attempts >= 3)
                {
                    Console.WriteLine("Account locked");
                }
                else
                {
                    Console.WriteLine("Try again");
                }
            }
        }
        else
        {
            Console.WriteLine("Unknown user");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("test case 1");
        Login("xxx", "xxx", 0);

        Console.WriteLine("test case 2");
        Login("admin", "1234", 0);

        Console.WriteLine("test case 3");
        Login("admin", "xxx", 4);

        Console.WriteLine("test case 4");
        Login("admin", "xxx", 0);
    }
}
