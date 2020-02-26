using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Responses
{
    public class CheckStateResponse : Response
    {
        public StateTax Tax { get; set; }
    }
}
