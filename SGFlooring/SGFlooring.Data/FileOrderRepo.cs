using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class FileOrderRepo : IOrderRepository
    {
        private static string _path;
        private static string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
        private static string tabDelimiter = "\t";

        public FileOrderRepo(string path) => _path = path;

        public void ReplaceOrder(Order order)
        {
            List<Order> ordersOnDate = GetAllOrdersOnDate(order.OrderDate).ToList();
            ordersOnDate.Remove(ordersOnDate.Single(o => o.OrderNumber == order.OrderNumber));
            ordersOnDate.Add(order);

            using (StreamWriter sw = new StreamWriter(GenerateFilePath(order.OrderDate)))
            {
                sw.WriteLine(header);

                foreach (var o in ordersOnDate)
                {
                    sw.Write(o.OrderNumber + tabDelimiter);
                    sw.Write(o.CustomerName + tabDelimiter);
                    sw.Write(o.StateTax.StateAbbreviation + tabDelimiter);
                    sw.Write(o.StateTax.TaxRate + tabDelimiter);
                    sw.Write(o.Product.ProductType + tabDelimiter);
                    sw.Write(o.Area + tabDelimiter);
                    sw.Write(o.Product.CostPerSquareFoot + tabDelimiter);
                    sw.Write(o.Product.LaborCostPerSquareFoot + tabDelimiter);
                    sw.Write(o.MaterialCost + tabDelimiter);
                    sw.Write(o.LaborCost + tabDelimiter);
                    sw.Write(o.TaxCost + tabDelimiter);
                    sw.WriteLine(o.Total);
                }
            }
        }

        public IEnumerable<Order> GetAllOrdersOnDate(DateTime orderDate)
        {
            List<Order> ordersOnDate = new List<Order>();
            FileInfo fi = new FileInfo(GenerateFilePath(orderDate));

            if (fi.Exists)
            {
                using (StreamReader sr = new StreamReader(fi.FullName))
                {
                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != null)
                        {
                            string[] row = line.Split('\t');

                            DateTime date = orderDate;
                            int orderNumber = int.Parse(row[0]);
                            string customerName = row[1];
                            string state = row[2];
                            decimal taxRate = decimal.Parse(row[3]);
                            string productType = row[4];
                            decimal area = decimal.Parse(row[5]);
                            decimal costPerSquareFoot = decimal.Parse(row[6]);
                            decimal laborCostPerSquareFoot = decimal.Parse(row[7]);
                            decimal materialCost = decimal.Parse(row[8]);
                            decimal laborCost = decimal.Parse(row[9]);
                            decimal taxCost = decimal.Parse(row[10]);
                            decimal total = decimal.Parse(row[11]);

                            Material product = new Material(productType, costPerSquareFoot, laborCostPerSquareFoot);
                            StateTax stateTaxInfo = new StateTax(state, "", taxRate);

                            Order order = new Order(orderDate, product, stateTaxInfo, orderNumber, customerName, area, materialCost, laborCost, taxCost, total);
                            ordersOnDate.Add(order);
                        }
                    }
                }
            }
            return ordersOnDate;
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            List<Order> ordersOnDate = GetAllOrdersOnDate(orderDate).ToList();
            return ordersOnDate.Single(o => o.OrderNumber == orderNumber);
        }

        public void RemoveOrder(Order order)
        {
            List<Order> ordersOnDate = GetAllOrdersOnDate(order.OrderDate).ToList();
            ordersOnDate.Remove(ordersOnDate.Single(o => o.OrderNumber == order.OrderNumber));

            using (StreamWriter sw = new StreamWriter(GenerateFilePath(order.OrderDate)))
            {
                sw.WriteLine(header);

                foreach (var o in ordersOnDate)
                {
                    sw.Write(o.OrderNumber + tabDelimiter);
                    sw.Write(o.CustomerName + tabDelimiter);
                    sw.Write(o.StateTax.StateAbbreviation + tabDelimiter);
                    sw.Write(o.StateTax.TaxRate + tabDelimiter);
                    sw.Write(o.Product.ProductType + tabDelimiter);
                    sw.Write(o.Area + tabDelimiter);
                    sw.Write(o.Product.CostPerSquareFoot + tabDelimiter);
                    sw.Write(o.Product.LaborCostPerSquareFoot + tabDelimiter);
                    sw.Write(o.MaterialCost + tabDelimiter);
                    sw.Write(o.LaborCost + tabDelimiter);
                    sw.Write(o.TaxCost + tabDelimiter);
                    sw.WriteLine(o.Total);
                }
            }
        }

        public void SaveOrder(Order order)
        {
            if (CheckIfOrderFileExists(order.OrderDate))
            {
                List<Order> ordersOnDate = GetAllOrdersOnDate(order.OrderDate).ToList();
                order.OrderNumber = ordersOnDate.Max(o => o.OrderNumber) + 1;
                ordersOnDate.Add(order);

                using (StreamWriter sw = new StreamWriter(GenerateFilePath(order.OrderDate)))
                {
                    sw.WriteLine(header);

                    foreach (var o in ordersOnDate)
                    {
                        sw.Write(o.OrderNumber + tabDelimiter);
                        sw.Write(o.CustomerName + tabDelimiter);
                        sw.Write(o.StateTax.StateAbbreviation + tabDelimiter);
                        sw.Write(o.StateTax.TaxRate + tabDelimiter);
                        sw.Write(o.Product.ProductType + tabDelimiter);
                        sw.Write(o.Area + tabDelimiter);
                        sw.Write(o.Product.CostPerSquareFoot + tabDelimiter);
                        sw.Write(o.Product.LaborCostPerSquareFoot + tabDelimiter);
                        sw.Write(o.MaterialCost + tabDelimiter);
                        sw.Write(o.LaborCost + tabDelimiter);
                        sw.Write(o.TaxCost + tabDelimiter);
                        sw.WriteLine(o.Total);
                    }
                }
            }
            else
            {
                order.OrderNumber = 1;


                using (StreamWriter sw = new StreamWriter(GenerateFilePath(order.OrderDate)))
                {
                    sw.WriteLine(header);
                    sw.Write(order.OrderNumber + tabDelimiter);
                    sw.Write(order.CustomerName + tabDelimiter);
                    sw.Write(order.StateTax.StateAbbreviation + tabDelimiter);
                    sw.Write(order.StateTax.TaxRate + tabDelimiter);
                    sw.Write(order.Product.ProductType + tabDelimiter);
                    sw.Write(order.Area + tabDelimiter);
                    sw.Write(order.Product.CostPerSquareFoot + tabDelimiter);
                    sw.Write(order.Product.LaborCostPerSquareFoot + tabDelimiter);
                    sw.Write(order.MaterialCost + tabDelimiter);
                    sw.Write(order.LaborCost + tabDelimiter);
                    sw.Write(order.TaxCost + tabDelimiter);
                    sw.WriteLine(order.Total);
                }
            }
        }

        public bool CheckIfOrderFileExists(DateTime date)
        {
            FileInfo fi = new FileInfo(GenerateFilePath(date));

            if (fi.Exists)
            {
                return true;
            }
            else return false;
        }

        private string GenerateFilePath(DateTime date) => _path + @"\" + "Orders_" + date.ToString("MMddyyyy") + ".txt";

    }

}



