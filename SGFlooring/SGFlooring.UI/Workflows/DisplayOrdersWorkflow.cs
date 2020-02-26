using SGFlooring.BLL;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.UI.Workflows
{
    class DisplayOrdersWorkflow
    {
        internal static void Run()
        {
            Console.Clear();
            ConsoleIO.TitleHeader("Lookup all orders on a given date");

            DateTime date = ConsoleIO.GetDateFromUser();
            Manager manager = ManagerFactory.Create();
            GetOrdersResponse response = manager.GetOrders(date);

            if(response.Success)
            {
                foreach(var order in response.OrdersOnDate)
                {
                    ConsoleIO.PrintOrder(order, true);
                }
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();

        }

    }
}
