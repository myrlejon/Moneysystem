using System;
using System.Collections.Generic;
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
                var input = Console.ReadLine();

                switch (Convert.ToInt32(input))
                {
                    case 1: // Login
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
                    case 2: // Exit program
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
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
                var input = Console.ReadLine();
                switch (Convert.ToInt32(input))
                {
                    case 1: // access user.salary
                        Console.Write("Salary: " + api.ViewSalary(currentUser.ID));
                        Console.ReadLine();
                        break;
                    case 2: // access user.role
                        Console.Write("Role: " + api.ViewRole(currentUser.ID));
                        Console.ReadLine();
                        break;
                    case 3: // remove account and log out
                        api.RemoveUser(currentUser.ID, currentUser.Name, currentUser.Password);
                        api.Logout(currentUser.ID);
                        currentUser = new Models.Account();
                        exit = true;
                        break;
                    case 4: // log out
                        api.Logout(currentUser.ID); 
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
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
                var input = Console.ReadLine();
                switch (Convert.ToInt32(input))
                {
                    case 1: // access user.salary
                        //api.ViewSalary() alternativt api.GetUser();
                        Console.WriteLine("Enter the ID of the user you want to get the salary from");
                        var salaryInput = Console.ReadLine();
                        int salaryInt = Convert.ToInt32(salaryInput);
                        string salary = api.ViewRole(salaryInt);
                        Console.WriteLine(salary);
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case 2: // access user.role
                        //api.ViewRole(); alternativt api.GetUser();
                        Console.WriteLine("Enter the ID of the user you want to get the role from");
                        var roleInput = Console.ReadLine();
                        int roleInt = Convert.ToInt32(roleInput);
                        string role = api.ViewRole(roleInt);
                        Console.WriteLine(role);
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case 3: // create a user 
                        //TODO: Denna metoden görs om sen med passwordchecks osv, gjorde den temporärt för att testa att den fungerar.
                        //api.CreateUser();
                        break;
                    case 4: // remove an user
                        bool remove = false;
                        Console.WriteLine("Enter the ID of the user you want to delete.");
                        var removeInput = Console.ReadLine();
                        int removeInt = Convert.ToInt32(removeInput);
                        remove = api.RemoveUserAdmin(currentUser.ID, removeInt);
                        if (remove)
                        {
                            Console.WriteLine("Succesfully removed user.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to remove user.");
                        }
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case 5: // list all users and passwords
                        List<Models.Account> list = new();
                        list = api.ListAllUsers();
                        foreach (var user in list)
                        {
                            System.Console.WriteLine($"ID: {user.ID} Username: {user.Name} Password: {user.Password}");
                        }
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case 6: // log out
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