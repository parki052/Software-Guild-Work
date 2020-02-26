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
    class AddOrderWorkflow
    {
        internal static void Run()
        {
            Manager manager = ManagerFactory.Create();
            var date = GetDate();
            var name = GetName();
            var tax = GetTax(manager);
            var product = GetProduct(manager);
            var area = GetArea();

            Console.Clear();
            Order order = new Order(date, product, tax, name, area);
            ConsoleIO.TitleHeader("Confirm Order");
            ConsoleIO.PrintOrder(order, false);

            bool confirmOrder = ConsoleIO.ConsoleKeyConfirmationSwitch("", true);
            if (confirmOrder)
            {
                AddOrderResponse addOrderResponse = manager.AddOrder(order);

                if (addOrderResponse.Success)
                {
                    Console.Clear();
                    Console.WriteLine($"Order for {order.CustomerName} for {order.OrderDate.ToString("MM/dd/yyyy")} saved successfully.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(addOrderResponse.Message);
                }
            }
            Console.Write("Press any key to continue.");
            Console.ReadKey(); 
        }

        private static DateTime GetDate()
        {
            Console.Clear();
            ConsoleIO.TitleHeader("Add an Order");
            Console.WriteLine("(Step 1 of 5)\n");

            DateTime date = new DateTime();
            bool validDate = false;
            while (!validDate)
            {
                date = ConsoleIO.GetDateFromUser();

                if (date < DateTime.Now)
                {

                    validDate = false;
                    Console.Write("Error: date must be in the future. Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else validDate = true;
            }
            return date;
        }

        private static string GetName()
        {
            Console.Clear();
            ConsoleIO.TitleHeader("Add an Order");
            Console.WriteLine("(Step 2 of 5)\n");
            return ConsoleIO.GetNameFromUser("Please enter a customer name for the order: ");
        }

        private static StateTax GetTax(Manager manager)
        {
            bool validState = false;
            StateTax tax = null;
            GetStatesResponse stateTaxesResponse = manager.GetStates();
            if (stateTaxesResponse.Success)
            {
                while (!validState)
                {
                    Console.Clear();
                    ConsoleIO.TitleHeader("Add an Order");
                    Console.WriteLine("(Step 3 of 5)\n");
                    ConsoleIO.PrintStateTaxInfo(stateTaxesResponse.StateTaxes);
                    string stateAbbreviation = ConsoleIO.GetStateFromUser();
                    CheckStateResponse stateResponse = manager.CheckForRequestedState(stateAbbreviation);
                    validState = stateResponse.Success;
                    if (!validState)
                    {
                        Console.Clear();
                        Console.WriteLine(stateResponse.Message);
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        tax = stateResponse.Tax;
                    }
                }
            }
            else
            {
                Console.WriteLine(stateTaxesResponse.Message);
            }
            return tax;
        }

        private static Material GetProduct(Manager manager)
        {
            bool validProduct = false;
            Material product = null;

            while (!validProduct)
            {
                Console.Clear();
                ConsoleIO.TitleHeader("Add an Order");
                Console.WriteLine("(Step 4 of 5)\n");

                GetProductsResponse getProductResponse = manager.GetProducts();
                if (getProductResponse.Success)
                {
                    ConsoleIO.PrintProducts(getProductResponse.Materials);
                }
                else
                {
                    Console.WriteLine(getProductResponse.Message);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }

                string userProductInput = ConsoleIO.GetProductFromUser();

                CheckProductResponse checkProdResponse = manager.CheckForRequestedProduct(userProductInput);

                if (checkProdResponse.Success == true)
                {
                    validProduct = true;
                    product = checkProdResponse.Product;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(checkProdResponse.Message);
                    Console.Write("Press any key to retry...");
                    Console.ReadKey();
                }
                if (validProduct)
                {
                    validProduct = ConsoleIO.ConsoleKeyConfirmationSwitch($"Entered product is ({userProductInput}).", false);
                }
            }
            return product;
        }

        private static decimal GetArea()
        {
            bool validArea = false;
            decimal area = 0;
            while (!validArea)
            {
                Console.Clear();
                ConsoleIO.TitleHeader("Add an Order");
                Console.WriteLine("(Step 5 of 5)\n");
                area = ConsoleIO.GetAreaFromUser();
                validArea = ConsoleIO.ConsoleKeyConfirmationSwitch($"Desired square footage for the order is {area}. ", false);
            }
            return area;
        }
    }
}
