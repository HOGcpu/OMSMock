using OMS.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OMS.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Models.Order> GetAllOrders();
        Models.Order GetOrderById(string orderId);
        void CreateOrder(Models.Order order);
        void UpdateOrder(Models.Order order);
        void DeleteOrder(string orderId);
        void Save();
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly OMSDbContext _context;

        public OrderRepository(OMSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Products)
                .Include(o => o.OrderDetails);
        }

        public Models.Order GetOrderById(string orderId)
        {
            return _context.Orders.Include(o => o.Products)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OmsId == orderId);
        }

        public void CreateOrder(Models.Order order)
        {
            _context.Orders.Add(order);
        }

        public void UpdateOrder(Models.Order order)
        {
            _context.Orders.Update(order);
        }

        public void DeleteOrder(string orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
