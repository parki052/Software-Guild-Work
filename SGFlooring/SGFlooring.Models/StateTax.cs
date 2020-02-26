using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models
{
    public class StateTax
    {
        public string StateAbbreviation { get; set; }
        public string StateName { get; set; }
        public decimal TaxRate { get; set; }

        public StateTax(string stateAbbreviation, string stateName, decimal taxRate)
        {
            StateAbbreviation = stateAbbreviation;
            StateName = stateName;
            TaxRate = taxRate;
        }
    }
}
