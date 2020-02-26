using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models
{
    public class Material
    {
        public string ProductType { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }


        public Material(string productType, decimal costPerSquareFoot, decimal laborCostPerSquareFoot)
        {
            ProductType = productType;
            CostPerSquareFoot = costPerSquareFoot;
            LaborCostPerSquareFoot = laborCostPerSquareFoot;
        }
    }
}
