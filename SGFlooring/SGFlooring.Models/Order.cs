using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public Material Product { get; set; }
        public StateTax StateTax { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal TaxCost { get; set; }
        public decimal Total { get; set; }


        public Order(DateTime date, Material product, StateTax tax, string name, decimal area)
        {
            OrderDate = date;
            Product = product;
            StateTax = tax;
            CustomerName = name;
            Area = area;

            MaterialCost = Math.Round((area * product.CostPerSquareFoot), 2);
            LaborCost = Math.Round((area * product.LaborCostPerSquareFoot), 2);
            TaxCost = Math.Round(((MaterialCost + LaborCost) * (tax.TaxRate / 100)), 2);
            Total = Math.Round((MaterialCost + LaborCost + TaxCost), 2);
        }

        public Order(Order oldOrder, string name, StateTax tax, Material product, decimal area)
        {
            OrderDate = oldOrder.OrderDate;
            OrderNumber = oldOrder.OrderNumber;
            CustomerName = name;
            StateTax = tax;
            Product = product;
            Area = area;

            MaterialCost = Math.Round((area * product.CostPerSquareFoot), 2);
            LaborCost = Math.Round((area * product.LaborCostPerSquareFoot), 2);
            TaxCost = Math.Round(((MaterialCost + LaborCost) * (tax.TaxRate / 100)), 2);
            Total = Math.Round((MaterialCost + LaborCost + TaxCost), 2);
        }

        public Order(DateTime orderDate, Material product, StateTax stateTax, int orderNumber, string customerName, decimal area, decimal materialCost, decimal laborCost, decimal taxCost, decimal total)
        {
            OrderDate = orderDate;
            Product = product;
            StateTax = stateTax;
            OrderNumber = orderNumber;
            CustomerName = customerName;
            Area = area;
            MaterialCost = materialCost;
            LaborCost = laborCost;
            TaxCost = taxCost;
            Total = total;
        }
    }
}
