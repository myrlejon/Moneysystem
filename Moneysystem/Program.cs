using System;
using Moneysystem.Database;
namespace Moneysystem
{
    class Program
    {
        static void Main()
        {
            Seeder.Seed();
            Menu.MainMenu();
        }
    }
}
