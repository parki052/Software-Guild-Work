using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class InMemoryOrderRepo : IOrderRepository
    {
        private static Order _order = new Order(
            new DateTime(2012, 5, 4),
            new Material("Wood", (decimal)5.15, (decimal)4.75),
            new StateTax("OH", "Ohio", (decimal)61.88),
            "Wise",
            100
            )
        {
            OrderNumber = 1
        };

        private static List<Order> _daysOrders = new List<Order> { _order };
        private static Dictionary<DateTime, List<Order>> _orderRepo = new Dictionary<DateTime, List<Order>> { { _order.OrderDate, _daysOrders } };

        public static void ResetRepo()
        {
            _order = new Order(
            new DateTime(2012, 5, 4),
            new Material("Wood", (decimal)5.15, (decimal)4.75),
            new StateTax("OH", "Ohio", (decimal)61.88),
            "Wise",
            100
            )
            {
                OrderNumber = 1
            };

            _daysOrders = new List<Order> { _order };
            _orderRepo = new Dictionary<DateTime, List<Order>> { { _order.OrderDate, _daysOrders } };
        }
        public void ReplaceOrder(Order order)
        {
            RemoveOrder(order);
            _orderRepo[order.OrderDate].Add(order);
        }

        public IEnumerable<Order> GetAllOrdersOnDate(DateTime orderDate) => _orderRepo[orderDate];

        public Order LoadOrder(DateTime orderDate, int orderNumber) => GetAllOrdersOnDate(orderDate).SingleOrDefault(o => o.OrderNumber == orderNumber);

        public void RemoveOrder(Order order) => _orderRepo[order.OrderDate].Remove(_orderRepo[order.OrderDate].SingleOrDefault(o => o.OrderNumber == order.OrderNumber));

        public void SaveOrder(Order order)
        {
            if (_orderRepo.ContainsKey(order.OrderDate))
            {
                order.OrderNumber = GetAllOrdersOnDate(order.OrderDate).Max(o => o.OrderNumber) + 1;
                _orderRepo[order.OrderDate].Add(order);
            }
            else
            {
                order.OrderNumber = 1;
                _orderRepo.Add(order.OrderDate, new List<Order> { order });
            }
        }
    }
}
