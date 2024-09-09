using System;

public class LoginManager
{
    private static LoginManager instance;
    private string loggedInUser;

    private LoginManager() {}

    public static LoginManager GetInstance()
    {
        if (instance == null)
        {
            instance = new LoginManager();
        }
        return instance;
    }

    public void Login(string username, string password)
    {
        if (username == "admin" && password == "password")
        {
            loggedInUser = username;
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Login failed.");
        }
    }

    public void Logout()
    {
        loggedInUser = null;
        Console.WriteLine("Logged out successfully.");
    }

    public bool IsLoggedIn()
    {
        return loggedInUser != null;
    }

    public string GetLoggedInUser()
    {
        return loggedInUser;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var loginManager = LoginManager.GetInstance();

        // login
        loginManager.Login("admin", "password");

        Console.WriteLine("Is logged in: " + loginManager.IsLoggedIn());
        Console.WriteLine("Logged in user: " + loginManager.GetLoggedInUser());

        // incorrect credentials
        loginManager.Login("admin", "wrongpassword");

        // logout
        loginManager.Logout();
        Console.WriteLine("Is logged in: " + loginManager.IsLoggedIn());
        Console.WriteLine("Logged in user: " + loginManager.GetLoggedInUser());

        var loginManager2 = LoginManager.GetInstance();
        loginManager2.Login("admin", "password");
        Console.WriteLine("Is logged in: " + loginManager.IsLoggedIn());

        // Output:
        // Time Elapsed 00:00:01.36
        // Login successful!
        // Is logged in: True
        // Logged in user: admin
        // Login failed.
        // Logged out successfully.  - this was logout for loginmanager 1
        // Is logged in: False       - loged out for loginmanager 1
        // Logged in user:           - logging in for loginmanager 2
        // Login successful!         - login for loginmanager 2
        // Is logged in: True        - user as well loged in for loginmanager 1, hence the instance is the same

        // Conclusion: The Singleton pattern ensures that only one instance of a class is created and provides a global point of access to that instance.
        
    }
}
