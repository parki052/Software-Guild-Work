using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Interfaces
{
    public interface ITaxRepository
    {
        IEnumerable<StateTax> GetStateTaxes();
    }
}
