using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrdersOnDate(DateTime orderDate);
        Order LoadOrder(DateTime orderDate, int orderNumber);
        void ReplaceOrder(Order order);
        void SaveOrder(Order order);
        void RemoveOrder(Order order);
    }
}
