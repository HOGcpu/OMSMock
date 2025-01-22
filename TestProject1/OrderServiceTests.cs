using NUnit.Framework;
using OMS.Models;
using OMS.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using OMS.Data;

namespace OMS.Tests
{
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private OMSDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Configure an in-memory database
            var options = new DbContextOptionsBuilder<OMSDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Initialize DbContext and Service
            _dbContext = new OMSDbContext(options);
            _orderService = new OrderService(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            // Clear in-memory database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void GetAllOrders_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Act
            var result = _orderService.GetAllOrders();

            // Assert
            Assert.IsEmpty(result, "Expected no orders, but some were returned.");
        }

        [Test]
        public void CreateOrder_ShouldAddOrderToDbContext()
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

        [Test]
        public void UpdateOrder_ShouldAddNewProductAndRemoveOldOne()
        {
            // Arrange
            var existingOrder = new Order
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

            _orderService.CreateOrder(existingOrder);

            var updatedProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    GTIN = "98765432109876",
                    Quantity = 3,
                    SerialNumberType = "SELF_MADE",
                    SerialNumbers = new List<string> { "SN006", "SN007", "SN008" },
                    TemplateId = 2
                }
            };

            var updatedOrder = new Order
            {
                OmsId = "OMS001",
                Products = updatedProducts,
                OrderDetails = existingOrder.OrderDetails
            };

            // Act
            _orderService.UpdateOrder("OMS001", updatedOrder);
            var result = _orderService.GetOrderById("OMS001");

            // Assert
            Assert.That(1 == result.Products.Count, "Product count does not match the updated order.");
            Assert.That("98765432109876" == result.Products.First().GTIN, "Product GTIN was not updated correctly.");
            Assert.That(3 == result.Products.First().Quantity, "Product quantity was not updated correctly.");
        }
    }
}
