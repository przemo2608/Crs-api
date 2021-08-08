using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public interface IOrder
    {
        void CreateOrder(string title, string description, int carId, int customerId, int mechanicId);
        void DeleteOrder(int orderId);
        IEnumerable<OrderDto> GetOrdersForWorker(int workerId);
        IEnumerable<OrderDto> GetOrdersForCustomer(int customerId);
        IEnumerable<OrderDto> GetOrders();
        void ChangeStatus(int orderId, string status);
    }
}
