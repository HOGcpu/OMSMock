using OMS.Repositories;
using System.Collections.Generic;

namespace OMS.Services
{
    public interface IOrderService
    {
        IEnumerable<Models.Order> GetAllOrders();
        Models.Order GetOrderById(string orderId);
        void CreateOrder(Models.Order order);
        void UpdateOrder(string orderId, Models.Order order);
        void DeleteOrder(string orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Models.Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        public Models.Order GetOrderById(string orderId)
        {
            return _repository.GetOrderById(orderId);
        }

        public void CreateOrder(Models.Order order)
        {
            _repository.CreateOrder(order);
            _repository.Save();
        }

        public void UpdateOrder(string orderId, Models.Order updatedOrder)
        {
            var existingOrder = _repository.GetOrderById(orderId);
            if (existingOrder != null)
            {
                existingOrder.Products = updatedOrder.Products;
                existingOrder.OrderDetails = updatedOrder.OrderDetails;
                _repository.UpdateOrder(existingOrder);
                _repository.Save();
            }
        }

        public void DeleteOrder(string orderId)
        {
            _repository.DeleteOrder(orderId);
            _repository.Save();
        }
    }
}
