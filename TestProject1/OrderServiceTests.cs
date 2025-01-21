using NUnit.Framework;
using OMS.Models;
using OMS.Repositories;
using OMS.Services;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Tests
{
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private MockOrderRepository _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Inicializiraj mock repozitorij in mock servis
            _mockRepository = new MockOrderRepository();
            _orderService = new OrderService(_mockRepository);
        }

        [Test]
        public void GetAllOrders_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Act
            var result = _orderService.GetAllOrders();

            // Assert
            Assert.IsEmpty(result, "Expected no orders, but some orders were returned.");
        }

        [Test]
        public void CreateOrder_ShouldAddOrderToRepository()
        {
            // Arrange
            var newOrder = new Order
            {
                OmsId = "OMS001",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        GTIN = "12345678901234",
                        Quantity = 5,
                        SerialNumberType = "SELF_MADE",
                        SerialNumbers = new List<string> { "SN001", "SN002", "SN003", "SN004", "SN005" },
                        TemplateId = 1
                    }
                },
                OrderDetails = new OrderDetails
                {
                    FactoryId = "F001",
                    FactoryName = "Factory A",
                    FactoryCountry = "Slovenia",
                    ProductionLineId = "PL001",
                    ProductCode = "P001",
                    ProductDescription = "Test Product"
                }
            };

            // Act
            _orderService.CreateOrder(newOrder);
            var allOrders = _orderService.GetAllOrders();

            // Assert
            Assert.That(allOrders.Count() == 1, "Order count should be 1 after adding a new order.");
            Assert.That("OMS001" == allOrders.First().OmsId, "The added order's OMS ID does not match.");
        }
    }

    // Mock Repository for testing
    public class MockOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public IEnumerable<Order> GetAllOrders() => _orders;

        public Order GetOrderById(string orderId) => _orders.Find(o => o.OmsId == orderId);

        public void CreateOrder(Order order) => _orders.Add(order);

        public void UpdateOrder(Order order)
        {
            var existingOrder = GetOrderById(order.OmsId);
            if (existingOrder != null)
            {
                _orders.Remove(existingOrder);
                _orders.Add(order);
            }
        }

        public void DeleteOrder(string orderId) => _orders.RemoveAll(o => o.OmsId == orderId);

        public void Save() { }
    }
}
