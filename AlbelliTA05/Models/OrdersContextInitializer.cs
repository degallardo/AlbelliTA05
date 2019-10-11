namespace AlbelliTA05.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class OrdersContextInitializer : DropCreateDatabaseAlways<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            var products = new List<Product>()
            {
                new Product() { Name = "Photo Book", Width = 19, MaxStack = 1 },
                new Product() { Name = "Calendar", Width = 10, MaxStack = 1 },
                new Product() { Name = "Canvas", Width = 16, MaxStack = 1 },
                new Product() { Name = "Greeting Cards", Width = 4.7, MaxStack = 1 },
                new Product() { Name = "Mug", Width = 94, MaxStack = 4 }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var order = new Order() { Customer = "Albelli", RequiredBinWidth = 78 };
            var od = new List<OrderDetail>()
            {
                new OrderDetail() { Product = products[0], Quantity = 2, Order = order },
                new OrderDetail() { Product = products[1], Quantity = 4, Order = order }
            };
            context.Orders.Add(order);
            od.ForEach(o => context.OrderDetails.Add(o));

            context.SaveChanges();
        }
    }
}