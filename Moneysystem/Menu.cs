using System;
using System.Collections.Generic;
using Moneysystem.Utilities;
namespace Moneysystem
{
    public static class Menu
    {
        static Models.Account currentUser = new();
        static API.API api = new();
        /// <summary>
        /// Creates a basic main menu that lets the user log in, or exit the program.
        /// </summary>
        public static void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to the main menu.");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1 - Log in as an User or Admin");
                Console.WriteLine("2 - Exit the program");
                Console.Write(">  ");
                // var input = Console.ReadLine();

                switch (Console.ReadLine())
                {
                    case "1": // Login
                        Console.WriteLine("Username: ");
                        var userInput = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        var passInput = Console.ReadLine();

                        currentUser = api.GetUser(api.Login(userInput, passInput));

                        if (currentUser is null)
                        {
                            Console.WriteLine("Wrong input.");
                            break;
                        }
                        else
                        {
                            if (currentUser.IsAdmin)
                            {
                                AdminMenu();
                            }
                            else
                            {
                                UserMenu();
                            }
                        }
                        break;
                    case "2": // Exit program
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please press enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
            Console.WriteLine("Thank you for using this program");
        }
        /// <summary>
        /// Prompts the user to perform some basic choices
        /// </summary>
        public static void UserMenu()
        {
            bool exit = false;
            Console.Clear();
            while (!exit)
            {
                Console.WriteLine("Welcome to the User menu.");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1 - See your current salary");
                Console.WriteLine("2 - See your role in the company");
                Console.WriteLine("3 - Remove your user account");
                Console.WriteLine("4 - Log out and return to the main menu");
                Console.Write(">  ");

                switch (Console.ReadLine())
                {
                    case "1": // access user.salary
                        Console.WriteLine("Salary: " + api.ViewSalary(currentUser.ID));
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "2": // access user.role
                        Console.WriteLine("Role: " + api.ViewRole(currentUser.ID));
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "3": // remove account and log out
                        Console.WriteLine("To remove your account, please enter your username and your password.");
                        Console.WriteLine("Username: ");
                        Console.Write("> ");
                        string inputName = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        Console.Write("> ");
                        string inputPwd = Console.ReadLine();
                        if(currentUser.Name.Equals(inputName) &&
                            inputPwd.Equals(currentUser.Password))
                        {
                            api.RemoveUser(currentUser.ID, currentUser.Name, currentUser.Password);
                            api.Logout(currentUser.ID);
                            currentUser = new ();
                            exit = true;
                            Console.WriteLine("You have removed your account, and are also logged out.");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect username and/or password. Returning to menu");
                        }
                        Console.ReadLine();
                        
                        break;
                    case "4": // log out
                        api.Logout(currentUser.ID); 
                        currentUser = new();
                        Console.WriteLine("You have logged out.");
                        Console.ReadLine();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Press enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        /// <summary>
        /// Prompts the user for commands from the admin menu
        /// </summary>
        public static void AdminMenu()
        {
            bool exit = false;
            Console.Clear();
            while (!exit)
            {
                Console.WriteLine("Welcome to the Admin menu.");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1 - See your current salary");
                Console.WriteLine("2 - See your role in the company");
                Console.WriteLine("3 - Create an user");
                Console.WriteLine("4 - Remove an user");
                Console.WriteLine("5 - List all users and passwords");
                Console.WriteLine("6 - Log out and return to the main menu");
                Console.Write(">  ");
                // var input = Console.ReadLine();
                switch (Console.ReadLine())
                {
                    case "1": // access user.salary
                        Console.WriteLine("Salary: " + api.ViewSalary(currentUser.ID));
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "2": // access user.role
                        Console.WriteLine("Role: " + api.ViewRole(currentUser.ID));
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "3": // create a user 
                        Console.WriteLine("Please enter a username:");
                        Console.Write("> ");
                        string username = Console.ReadLine();
                        if(string.IsNullOrEmpty(username) || username.Length < 3)
                        {
                            Console.WriteLine("\nPlease enter a username with at least 3 characters"+
                            " containing at least 1 letter and 1 number");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Please enter a password:");
                        Console.Write("> ");
                        string pwd1 = Console.ReadLine();
                        Console.WriteLine("\nPlease verify the password:");
                        Console.Write("> ");
                        string pwd2 = Console.ReadLine();
                        Console.WriteLine("Please enter one of the avaiable roles for the user: \n");
                        foreach(var item in Utilities.Roles.ListOfRoles){
                            Console.WriteLine("\t" + item);
                        }
                        Console.WriteLine();
                        string userrole = Console.ReadLine();
                        int usersalary = 0;
                        Console.WriteLine("Please enter a valid starting salary\n" +
                        "(or just press enter for basic salary for that role)");
                        string salaryString = Console.ReadLine();
                        Int32.TryParse(salaryString, out usersalary);
                        Console.WriteLine("Attempting to create user " + username);
                        if (api.CreateUser(username, pwd1, pwd2, userrole, usersalary))
                        {
                            Console.WriteLine(username + " was created!");
                        }
                        else
                        {
                            Console.WriteLine("Sorry. Something went wrong. " +
                             username + " was not created");
                        }
                        Console.WriteLine("Returning to menu ...");
                        Console.ReadLine();
                        break;
                    case "4": // remove an user
                        Console.WriteLine("Please enter the username of the user to remove: ");
                        Console.Write("> ");
                        string removeUser = Console.ReadLine();
                        Console.WriteLine("\nPlease enther the password of the user to remove: ");
                        Console.Write("> ");
                        string removePassword = Console.ReadLine();
                        var userToRemove = api.GetUser(removeUser);
                        if(userToRemove is not null)
                        {
                            if(api.RemoveUser(userToRemove.ID, removeUser, removePassword))
                            {
                                Console.WriteLine("You removed user " + removeUser);
                            }
                            else
                            {
                                Console.WriteLine("User " + removeUser + " was not removed");
                            }
                        }
                        else
                        {
                            Console.WriteLine(removeUser + " was an incorrect username. Please try again");
                        }
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "5": // list all users and passwords
                        List<Models.Account> list = api.ListAllUsers();
                        foreach (var user in list)
                        {
                            System.Console.WriteLine($"ID: {user.ID} Username: {user.Name} Password: {user.Password}");
                        }
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case "6": // log out
                        api.Logout(currentUser.ID);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}