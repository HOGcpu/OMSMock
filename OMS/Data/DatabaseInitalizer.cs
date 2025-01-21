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
            var order = new Order
            {
                OmsId = "OMS001",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        GTIN = "12345678901234",
                        Quantity = 5,
                        SerialNumberType = "SELF_MADE",
                        SerialNumbers = new List<string> { "SN001", "SN002", "SN003" },
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

            context.Orders.Add(order);

            context.SaveChanges();
        }
    }
}
