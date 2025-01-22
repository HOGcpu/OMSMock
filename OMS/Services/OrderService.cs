using Microsoft.EntityFrameworkCore;
using OMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Services
{
    public interface IOrderService
    {
        IEnumerable<Models.Order> GetAllOrders();
        Models.Order GetOrderById(string orderId);
        void CreateOrder(Models.Order order);
        void UpdateOrder(string orderId, Models.Order updatedOrder);
        void DeleteOrder(string orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly OMSDbContext _context;

        public OrderService(OMSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Products)
                .Include(o => o.OrderDetails)
                .ToList();
        }

        public Models.Order GetOrderById(string orderId)
        {
            return _context.Orders
                .Include(o => o.Products)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OmsId == orderId);
        }

        public void CreateOrder(Models.Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(string orderId, Models.Order updatedOrder)
        {
            var existingOrder = GetOrderById(orderId);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with ID '{orderId}' not found.");
            }

            // Update OrderDetails
            if (updatedOrder.OrderDetails != null) {
                _context.Entry(existingOrder.OrderDetails).CurrentValues.SetValues(updatedOrder.OrderDetails);
            }
            else
            {
                _context.Remove(existingOrder.OrderDetails);
                existingOrder.OrderDetails = null;
            }
            // update or Add Products
            foreach (var updatedProduct in updatedOrder.Products)
            {
                var existingProduct = existingOrder.Products.FirstOrDefault(p => p.GTIN == updatedProduct.GTIN);

                if (existingProduct != null)
                {
                    _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                }
                else
                {
                    existingOrder.Products.Add(updatedProduct);
                }
            }

            // Remove Products that are not present in updatedOrder
            foreach (var existingProduct in existingOrder.Products.ToList())
            {
                if (!updatedOrder.Products.Any(p => p.GTIN == existingProduct.GTIN))
                {
                    _context.Entry(existingProduct).State = EntityState.Deleted;
                }
            }

            _context.SaveChanges();
        }

        public void DeleteOrder(string orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);

                _context.SaveChanges();
            }
        }
    }
}
