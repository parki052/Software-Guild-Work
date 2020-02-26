using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class InMemoryTaxRepo : ITaxRepository
    {
        private static List<StateTax> _taxInfo = new List<StateTax>
        {
            new StateTax("OH", "Ohio", 6.25m),
            new StateTax("PA", "Pennsylvania", 6.75m),
            new StateTax("MI", "Michigan", 5.75m),
            new StateTax("IN",  "Indiana", 6.00m)
        };
        public IEnumerable<StateTax> GetStateTaxes() => _taxInfo;

    }
}
