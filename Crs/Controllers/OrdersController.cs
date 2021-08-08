using Crs.Authorization;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
       
        private IOrder orderService;

        public OrdersController (IOrder orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost]
        [Route("createOrder")]
        public IActionResult CreateOrder(CreateOrderModel createOrderModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            orderService.CreateOrder(createOrderModel.Title, createOrderModel.Description, createOrderModel.CarId, createOrderModel.CustomerId, createOrderModel.MechanicId);
            return new OkResult();

        }

        [HttpDelete]
        [Route("deleteOrder")]
        public IActionResult DeleteOrder(DeleteOrderModel deleteOrderModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            orderService.DeleteOrder(deleteOrderModel.OrderId);

            return new OkResult();
        }

        [HttpPut]
        [Route("changeOrderStatus")]
        public IActionResult ChangeOrderStatus(ChangeOrderStatusModel changeOrderStatusModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            orderService.ChangeStatus(changeOrderStatusModel.orderId, changeOrderStatusModel.Status);

            return new OkResult();
        }

        [HttpGet]
        [Route("getOrders")]
        public IEnumerable<OrderModel> GetOrders()
        {
            var orders = orderService.GetOrders();

            return orders.Select(x => new OrderModel()
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status
            });
        }
        [MechanicRole]
        [HttpGet]
        [Route("getOrdersForWorker/{id}")]
        public IEnumerable<OrderModel> GetOrdersForWorker(int id)
        {
            var orders = orderService.GetOrdersForWorker(id);

            return orders.Select(x => new OrderModel()
            {
                OrderId = x.OrderId,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CarBrand = x.CarBrand,
                CarModel = x.CarModel
            });
        }
        [CustomerRole]
        [HttpGet]
        [Route("getOrdersForCustomer/{id}")]
        public IEnumerable<OrderModel> GetOrdersForCustomer(int id)
        {
            var orders = orderService.GetOrdersForCustomer(id);

            return orders.Select(x => new OrderModel()
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CarBrand = x.CarBrand,
                CarModel = x.CarModel
            });
        }
    }
}
