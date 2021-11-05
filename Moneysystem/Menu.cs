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


    }
}