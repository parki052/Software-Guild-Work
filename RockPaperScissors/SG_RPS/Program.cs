using SG_RPS.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playerExit = false;
            while (!playerExit)
            {
                GameFlow.Run();

                Console.WriteLine("\n\nPress any key to play again, or press q to quit.");
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Q)
                {
                    playerExit = true;
                }
            }
            Environment.Exit(0);
        }
    }
}
