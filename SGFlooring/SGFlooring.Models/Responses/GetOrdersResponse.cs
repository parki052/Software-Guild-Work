using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Responses
{
    public class GetOrdersResponse : Response
    {
        public List<Order> OrdersOnDate { get; set; }
    }
}
