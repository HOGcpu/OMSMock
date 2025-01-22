using OMS.Data;
using OMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DatabaseInitializer
{
    public static void Seed(OMSDbContext context)
    {
        if (!context.Orders.Any())
        {
            // Prvi zapis
            var order1 = new Order
            {
                OmsId = "OMS002",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        GTIN = "12345678901234",
                        Quantity = 3,
                        SerialNumberType = "SELF_MADE",
                        SerialNumbers = new List<string> { "SN001", "SN002", "SN003" },
                        TemplateId = 1
                    }
                },
                OrderDetails = new OrderDetails
                {
                    FactoryId = "F002",
                    FactoryName = "Factory B",
                    FactoryCountry = "Fin",
                    ProductionLineId = "PL001",
                    ProductCode = "P001",
                    ProductDescription = "Test Product"
                }
            };

            // Drugi zapis
            var order2 = new Order
            {
                OmsId = "OMS003",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        GTIN = "56789012345678",
                        Quantity = 5,
                        SerialNumberType = "SELF_MADE",
                        SerialNumbers = new List<string> { "SN101", "SN102", "SN103", "SN104", "SN105" },
                        TemplateId = 2
                    },
                    new OrderProduct
                    {
                        GTIN = "89012345678901",
                        Quantity = 5,
                        SerialNumberType = "AUTO_GENERATED",
                        TemplateId = 3
                    }
                },
                OrderDetails = null
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);

            context.SaveChanges();
        }
    }
}
