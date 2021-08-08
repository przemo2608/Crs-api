using Crs.Data;
using Crs.Exceptions;
using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public class OrderService : IOrder
    {
        private readonly ICrsContext db;


        public OrderService(ICrsContext db)
        {

            this.db = db;
        }


        public void ChangeStatus(int orderId, string status)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                throw new OrderNotFoundException();
            else
            order.Status = status;
            db.Orders.Update(order);
            db.SaveChanges();
        }

        public void CreateOrder(string title, string description, int carId, int customerId, int mechanicId)
        {
            var customer = db.Users.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
                throw new ObjectNotFoundException();

            var mechanic = db.Users.FirstOrDefault(x => x.Id == mechanicId);
            if (mechanic == null)
                throw new ObjectNotFoundException();

            var car = db.Cars.FirstOrDefault(x => x.Id == carId);
            if (car == null)
                throw new ObjectNotFoundException();

            var order = new OrderDb 
            {
             Title = title,
             Description = description,
             CarId = car.Id,
             CustomerId = customer.Id,
             MechanicId = mechanic.Id,
             User = customer,
             Status = "w toku",
             CarBrand = car.CarBrand,
             CarModel = car.Model
            
            };
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                throw new OrderNotFoundException();
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = db.Orders.Where(x => x.Status == "w toku").ToList();

            return orders.Select(x => new OrderDto
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                
            });
        }

        public IEnumerable<OrderDto> GetOrdersForCustomer(int customerId)
        {
            var orders = db.Orders.Where(x => x.CustomerId == customerId && x.Status == "zakonczone").ToList();

            return orders.Select(x => new OrderDto
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CarBrand = x.CarBrand,
                CarModel = x.CarModel
            });
        }

        public IEnumerable<OrderDto> GetOrdersForWorker(int workerId)
        {
            var orders = db.Orders.Where(x => x.MechanicId == workerId && x.Status == "w toku").ToList();

            return orders.Select(x => new OrderDto
            {
                OrderId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CarBrand = x.CarBrand,
                CarModel = x.CarModel
            });
        }
    }
}
