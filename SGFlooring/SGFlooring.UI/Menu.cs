using SGFlooring.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.UI
{
    static class Menu
    {
        internal static void Run()
        {
            bool userExit = false;
            while (!userExit)
            {
                Console.Clear();
                ConsoleIO.TitleHeader("SG Flooring System");
                ConsoleIO.Separator();
                Console.WriteLine();
                Console.WriteLine(" 1. Display Orders");
                Console.WriteLine(" 2. Add an Order");
                Console.WriteLine(" 3. Edit an Order");
                Console.WriteLine(" 4. Remove an Order");
                Console.WriteLine(" 5. Quit");
                Console.WriteLine();
                ConsoleIO.Separator();
                Console.WriteLine();
                Console.Write("Enter a number to make a selection: ");

                ConsoleKeyInfo cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        DisplayOrdersWorkflow.Run();
                        break;

                    case ConsoleKey.D2:
                        AddOrderWorkflow.Run();
                        break;

                    case ConsoleKey.D3:
                        EditOrderWorkflow.Run();
                        break;

                    case ConsoleKey.D4:
                        RemoveOrderWorkflow.Run();
                        break;

                    case ConsoleKey.D5:
                        userExit = true;
                        Console.Clear();
                        Console.Write("Press any key to exit...");
                        Console.ReadKey();
                        break;
                }
            }

        }
    }
}
