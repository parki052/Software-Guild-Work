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
    class EditOrderWorkflow
    {
        internal static void Run()
        {
            Order oldOrder = LookupOrder();

            string name = EditCustomerName(oldOrder.CustomerName);
            StateTax tax = EditState(oldOrder.StateTax);
            Material product = EditProduct(oldOrder.Product);
            decimal area = EditArea(oldOrder.Area);

            Order newOrder = new Order(oldOrder, name, tax, product, area);

            Console.Clear();
            ConsoleIO.PrintOrder(newOrder, true);
            bool saveEditedOrder = ConsoleIO.ConsoleKeyConfirmationSwitch("Would you like to replace the old order with this updated order?", true);
            if (saveEditedOrder)
            {
                Console.Clear();
                Manager manager = ManagerFactory.Create();
                EditOrderResponse editResponse = manager.EditOrder(newOrder);

                if(editResponse.Success)
                {
                    Console.WriteLine("Order updated successfully.");
                }
                else
                {
                    Console.WriteLine(editResponse.Message);
                }
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private static Order LookupOrder()
        {
            Console.Clear();
            ConsoleIO.TitleHeader("Edit an Order");
            Console.WriteLine();

            bool foundAnOrder = false;
            Order order = null;
            while (!foundAnOrder)
            {
                DateTime date = ConsoleIO.GetDateFromUser();
                int orderNum = ConsoleIO.GetIntFromUser("Please enter an order number: ");
                Manager manager = ManagerFactory.Create();

                GetOrdersResponse response = manager.GetOrders(date);
                try
                {
                    order = response.OrdersOnDate.Single(o => o.OrderNumber == orderNum);
                    foundAnOrder = true;
                }
                catch
                {
                    Console.WriteLine("Could not find an order with that date and order number. Press any key to try again...");
                    Console.ReadKey();
                }
            }
            return order;
        }

        private static string EditCustomerName(string oldName)
        {
            string toReturn = "";

            Console.Clear();
            Console.Write($"Enter a new customer name, or press enter to keep ({oldName}) as the name: ");
            toReturn = Console.ReadLine();

            if (toReturn == "")
            {
                toReturn = oldName;
            }
            return toReturn;
        }

        private static StateTax EditState(StateTax oldStateTax)
        {
            Manager manager = ManagerFactory.Create();

            StateTax stateTax = oldStateTax;
            bool validInput = false;
            string userInput = "";
            while (!validInput)
            {
                Console.Clear();
                Console.Write($"Please enter a state abbreviation, or press enter to keep ({oldStateTax.StateAbbreviation}) as the order state: ");
                userInput = Console.ReadLine();
                if (userInput == "")
                {
                    validInput = true;
                }
                else
                {
                    CheckStateResponse response = manager.CheckForRequestedState(userInput);

                    if (response.Success)
                    {
                        validInput = true;
                        stateTax = response.Tax;
                    }
                    else
                    {
                        Console.WriteLine(response.Message);
                        Console.Write("Press any key to retry...");
                        Console.ReadKey();
                    }
                }
            }
            return stateTax;
        }

        private static Material EditProduct(Material oldProduct)
        {
            Manager manager = ManagerFactory.Create();
            GetProductsResponse productResponse = manager.GetProducts();

            Material product = oldProduct;
            bool validInput = false;
            string userInput = "";
            while (!validInput)
            {
                Console.Clear();
                ConsoleIO.PrintProducts(productResponse.Materials);
                Console.Write($"Please choose a product type, or press enter to keep ({oldProduct.ProductType}) as the order material: ");
                userInput = Console.ReadLine();
                if (userInput == "")
                {
                    validInput = true;
                }
                else
                {
                    CheckProductResponse response = manager.CheckForRequestedProduct(userInput);
                    if (response.Success)
                    {
                        validInput = true;
                        product = response.Product;
                    }
                    else
                    {
                        Console.WriteLine(response.Message);
                        Console.Write("Press any key to retry... ");
                        Console.ReadKey();
                    }
                }
            }
            return product;
        }

        private static decimal EditArea(decimal oldArea)
        {

            bool validInput = false;
            decimal area = decimal.MinValue;
            string userInput = "";
            while (!validInput)
            {
                Console.Clear();
                Console.Write($"Please enter in a new area for the order, or press enter to keep ({oldArea}) for the order: ");
                userInput = Console.ReadLine();

                if (userInput == "")
                {
                    area = oldArea;
                    validInput = true;
                }
                else
                {
                    validInput = decimal.TryParse(userInput, out area);

                    if (validInput)
                    {
                        if (area < 100)
                        {
                            validInput = false;
                            Console.Write("Error: area for orders must be greater to or equal to 100. Press any key to retry...");
                            Console.ReadKey();
                        }
                        else
                        {
                            validInput = true;
                        }
                    }
                }
            }
            return area;
        }
    }
}
