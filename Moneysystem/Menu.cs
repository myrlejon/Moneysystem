using System;
using System.Collections.Generic;
namespace Moneysystem
{
    public static class Menu
    {
        static Models.Account currentUser = new();
        static API.API api = new();

        static List<Models.Account> accounts = new List<Models.Account>
        {
            new Models.Admin {ID = 1, Name = "admin1", Password = "admin1234", IsAdmin = true , Salary = 500, Role = "Administrator"},
            new Models.User {ID = 2, Name = "user1", Password = "user1234", IsAdmin = false , Salary = 100, Role = "User"},
        };

        static List<string> roles = new List<string>() {"Minesweeper", "User", "Administrator", "Production", "Operations", "Manager", "Executive"};

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

                        currentUser = api.GetUser(userInput, passInput, accounts);

                        if (currentUser is null || api.Login(userInput, passInput, accounts) == false)
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
                        Console.Write("Salary: " + api.ViewSalary(currentUser.ID, accounts));
                        Console.ReadLine();
                        break;
                    case 2: // access user.role
                        Console.Write("Role: " + api.ViewRole(currentUser.ID, accounts));
                        Console.ReadLine();
                        break;
                    case 3: // remove account and log out
                        Console.WriteLine("Username: ");
                        var removeUserInput = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        var removePassInput = Console.ReadLine();
                        if (removeUserInput == currentUser.Name && removePassInput == currentUser.Password)
                        {
                            accounts.Remove(currentUser);
                            Console.WriteLine($"{removeUserInput} has been removed.");
                            currentUser = new Models.Account();
                        }
                        else
                        {
                            Console.WriteLine("Error removing user.");
                        }
                        exit = true;
                        break;
                    case 4: // log out
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
                        Console.Write("Salary: " + api.ViewSalary(currentUser.ID, accounts));
                        Console.ReadLine();
                        break;
                    case 2: // access user.role
                        Console.Write("Role: " + api.ViewRole(currentUser.ID, accounts));
                        Console.ReadLine();
                        break;
                    case 3: // create a user 
                        Console.Write("(Both username and password needs to have atleast one number and letter in them)\nUsername: ");
                        var createNameInput = Console.ReadLine();
                        Console.Write("Password: ");
                        var createPassInput = Console.ReadLine();
                        bool checkUsername = Utilities.PasswordChecker.CheckPassword(createNameInput);
                        bool checkPassword = Utilities.PasswordChecker.CheckPassword(createPassInput);

                        for (int i = 0; i < roles.Count; i++)
                        {
                            Console.WriteLine($"({i}) {roles[i]}");
                        }

                        Console.WriteLine("Enter the number of the role: ");
                        var createRoleInput = Console.ReadLine();
                        int createRoleInputInt = Convert.ToInt32(createRoleInput);
                        int createSalary = Utilities.Roles.SetBasicSalary(roles[createRoleInputInt]);
                        int createId = api.CreateID(accounts);
                        bool createAdmin = Utilities.Roles.SetIsAdmin(roles[createRoleInputInt]);

                        //Skapar user om rollen stämmer överrens
                        if (checkUsername && checkPassword && createAdmin == false)
                        {
                            var newUser = new Models.User() {ID = createId, Name = createNameInput, Password = createPassInput, IsAdmin = createAdmin, Salary = createSalary, Role = roles[createRoleInputInt]};
                            accounts.Add(newUser);
                            Console.WriteLine("Successfully created a new user.");
                        }
                        //Skapar admin om rollen stämmer överrens
                        else if (checkUsername && checkPassword && createAdmin)
                        {
                            var newUser = new Models.Admin() {ID = createId, Name = createNameInput, Password = createPassInput, IsAdmin = createAdmin, Salary = createSalary, Role = roles[createRoleInputInt]};
                            accounts.Add(newUser);
                            Console.WriteLine("Successfully created a new user.");
                        }
                        else
                        {
                            Console.WriteLine("Error creating user.");
                        }

                        break;
                    case 4: // remove an user //TODO: gör om metod
                        bool remove = false;
                        Console.WriteLine("Enter the ID of the user you want to delete.");
                        var removeInput = Console.ReadLine();
                        int removeInt = Convert.ToInt32(removeInput);
                        

                        if (removeInt != 1) 
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
                        foreach (var user in accounts)
                        {
                            Console.WriteLine($"ID: {user.ID} Username: {user.Name} Password: {user.Password}");
                        }
                        Console.WriteLine("Press any key to proceed...");
                        Console.ReadLine();
                        break;
                    case 6: // log out
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