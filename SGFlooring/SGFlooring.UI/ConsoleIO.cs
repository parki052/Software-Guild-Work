using SGFlooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.UI
{
    static class ConsoleIO
    {
        public static void Separator() => Console.WriteLine("*****************************************");

        public static DateTime GetDateFromUser()
        {
            bool validInput = false;
            DateTime date = new DateTime();

            while (!validInput)
            {
                Console.Write($"Please enter a date (MM/DD/YYYY): ");
                validInput = DateTime.TryParse(Console.ReadLine(), out date);

                if (!validInput)
                {
                    Console.Write("Invalid date input. Press any key to retry...");
                    Console.ReadKey();
                }
                else
                {
                    validInput = ConsoleKeyConfirmationSwitch($"\nEntered date is [{date.ToString("MM/dd/yyyy")}]", false);
                }
                Console.Clear();
            }
            return date;
        }

        internal static int GetIntFromUser(string message)
        {
            bool validInput = false;
            int toReturn = int.MinValue;

            while (!validInput)
            {
                Console.Write(message);
                validInput = int.TryParse(Console.ReadLine(), out toReturn);

                if (toReturn <= 0)
                {
                    validInput = false;
                }
                if (!validInput)
                {
                    Console.WriteLine("Error: invalid input. Press any key to retry...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            return toReturn;
        }

        internal static string GetStateFromUser()
        {
            bool validInput = false;
            string userInput = "";
            while (!validInput)
            {
                Console.Write("Please enter your state abbreviation (ex. AZ, TX): ");
                userInput = Console.ReadLine();
                if (userInput != "" && userInput != null)
                {
                    validInput = true;
                }
                else
                {
                    Console.Write("Error: this field cannot be empty. Press any key to retry...");
                    Console.ReadKey();
                    Console.Clear();
                }
                ConsoleKeyConfirmationSwitch($"\nEntered state abbreviation is [{userInput}].", false);
                Console.Clear();
            }
            return userInput;
        }

        internal static string GetNameFromUser(string message)
        {
            bool validInput = false;
            string userInput = "";

            while (!validInput)
            {
                Console.Write(message);
                userInput = Console.ReadLine();

                if (userInput != "" && userInput != null)
                {
                    validInput = true;
                }
                else
                {
                    Console.Write("Error: this field cannot be empty. Press any key to retry...");
                    Console.ReadKey();
                    Console.Clear();
                }
                validInput = ConsoleKeyConfirmationSwitch($"Entered name is ({userInput}).", false);
                Console.Clear();
            }
            return userInput;
        }

        internal static decimal GetAreaFromUser()
        {
            bool validInput = false;
            decimal area = 0;

            while (!validInput)
            {
                Console.Write("Please enter the desired square footage of area: ");
                validInput = decimal.TryParse(Console.ReadLine(), out area);

                if (area < 100)
                {
                    validInput = false;
                    Console.WriteLine("Error: area must be greater than 100. Press any key to retry...");
                    Console.ReadKey();
                }

            }
            return area;
        }

        internal static string GetProductFromUser()
        {
            bool validInput = false;
            string userInput = "";

            while (!validInput)
            {
                Console.Write("Please enter a product type: ");
                userInput = Console.ReadLine();

                if (userInput != "" && userInput != null)
                {
                    validInput = true;
                }
                else
                {
                    Console.Write("Error: this field cannot be empty. Press any key to retry...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            return userInput;
        }

        internal static bool ConsoleKeyConfirmationSwitch(string message, bool confirmingOrder)
        {
            bool validKey = false;
            bool toReturn = false;

            Console.WriteLine(message);
            if (!confirmingOrder)
            {
                Console.Write("\nPress Enter key to confirm, or press Esc key to re-enter input:");
            }
            else
            {
                Console.Write("\nPress Enter key to confirm, or press Esc to exit without saving:");
            }
            while (!validKey)
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.Enter:
                        validKey = true;
                        toReturn = true;
                        break;

                    case ConsoleKey.Escape:
                        validKey = true;
                        toReturn = false;
                        break;

                    default:
                        validKey = false;
                        break;
                }
            }
            return toReturn;
        }

        internal static void PrintOrder(Order order, bool hasOrderNumber)
        {
            string appendOrderNumber = "";
            if (hasOrderNumber)
            {
                appendOrderNumber = $"Order #{order.OrderNumber} | ";
            }
            Separator();

            Console.WriteLine($"{appendOrderNumber}{order.OrderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Name: {order.CustomerName}");
            Console.WriteLine($"State: {order.StateTax.StateAbbreviation}");
            Console.WriteLine($"Product: {order.Product.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.TaxCost:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Separator();
        }

        internal static void PrintProducts(List<Material> materials)
        {
            foreach (var product in materials)
            {
                Separator();
                Console.WriteLine($"Product type: {product.ProductType}");
                Console.WriteLine($"Cost per square foot: {product.CostPerSquareFoot:c}");
                Console.WriteLine($"Labor cost per square foot: {product.LaborCostPerSquareFoot:c}");
                Separator();
            }
        }

        internal static void PrintStateTaxInfo(List<StateTax> stateTaxList)
        {
            foreach(var state in stateTaxList)
            {
                Separator();
                Console.WriteLine($"State: {state.StateName} ({state.StateAbbreviation})");
                Separator();
            }
        }

        internal static void TitleHeader(string headerTitle)
        {
            Console.Write("        ");
            for (int i = 1; i <= headerTitle.Length + 8; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine($"\n        **  {headerTitle}  **");
            Console.Write("        ");
            for (int i = 1; i <= headerTitle.Length + 8; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("\n");
        }
    }
}
