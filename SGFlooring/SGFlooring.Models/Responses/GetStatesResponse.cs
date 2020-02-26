using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Responses
{
    public class GetStatesResponse : Response
    {
        public List<StateTax> StateTaxes { get; set; }
    }
}
