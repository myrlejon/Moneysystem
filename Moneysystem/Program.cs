using System;
namespace Moneysystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.Seeder.Seed();
            Menu.MainMenu();
        }
    }
}
