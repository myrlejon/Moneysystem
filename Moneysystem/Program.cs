using System;

namespace Moneysystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(1, 3);
            Test(0, 0);
        }

        static int Test(int a, int b)
        {
            int c = 0;
            c = a / b;
            return c;
        }
    }
}
