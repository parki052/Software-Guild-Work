using SGFlooring.BLL;
using SGFlooring.Models;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.UI.Workflows
{
    class RemoveOrderWorkflow
    {
        internal static void Run()
        {
            Console.Clear();
            ConsoleIO.TitleHeader("Remove an Order");
            Console.WriteLine();
            DateTime date = ConsoleIO.GetDateFromUser();
            int orderNum = ConsoleIO.GetIntFromUser("Please enter an order number: ");

            Manager manager = ManagerFactory.Create();
            GetOrdersResponse getOrderResponse = manager.GetOrders(date);
            Order toRemove;

            try
            {
                toRemove = getOrderResponse.OrdersOnDate.Single(o => o.OrderNumber == orderNum);
            }
            catch
            {
                Console.Clear();
                Console.Write("No order with that date and order number could be found. Press any key to continue... ");
                Console.ReadKey();
                toRemove = null;
            }

            if (toRemove != null)
            {
                Console.Clear();
                ConsoleIO.PrintOrder(toRemove, true);
                bool userConfirmsRemove = ConsoleIO.ConsoleKeyConfirmationSwitch("Would you like to remove this order from the repository?", false);

                if (userConfirmsRemove)
                {
                    RemoveOrderResponse response = manager.RemoveOrder(toRemove);
                    Console.Clear();
                    if (response.Success)
                    {
                        Console.Write($"Successfully removed order #{response.Order.OrderNumber} for {response.Order.CustomerName} on {response.Order.OrderDate.ToString("MM/dd/yyyy")}. Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write(response.Message + "\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
