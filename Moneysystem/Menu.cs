using System;


namespace Moneysystem
{
    public static class Menu
    {
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
                    case 1:
                        break;
                    case 2:
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
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        exit = true;
                        break;
                    case 4:
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