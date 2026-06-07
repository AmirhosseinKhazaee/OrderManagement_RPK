using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Customers.AnyAsync())
            return;

        var customer1 = new Customer
        {
            Name = "Ali Ahmadi",
            Email = "ali@test.com",
            Orders = new List<Order>()
        };
        var customer2 = new Customer
        {
            Name = "Amirhossein Khazaee",
            Email = "khazaeeaslamirhossein@gmail.com",
            Orders = new List<Order>()
        };
        var customer3 = new Customer
        {
            Name = "Matin Zamani",
            Email = "matin@test.com",
            Orders = new List<Order>()
        };
        var order1 = new Order
        {
            Title = "Gaming Setup",
            Detail = "High-end PC",
            TotalPrice = 1500,
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "GPU",
                    Quantity = 1,
                    UnitPrice = 1000
                },
                new OrderItem
                {
                    ProductName = "CPU",
                    Quantity = 1,
                    UnitPrice = 500
                }
            }
        };
        var order2 = new Order
        {
            Title = "Monitor",
            Detail = "High Resulotion",
            TotalPrice = 800,
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "144Hz Monitor",
                    Quantity = 1,
                    UnitPrice = 800
                }
            }
        };
        var order3 = new Order
        {
            Title = "Mouse & Keyboard",
            Detail = "Razor Mouse & Keyboard",
            TotalPrice = 1400,
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Keyboard",
                    Quantity = 1,
                    UnitPrice = 800
                },
                new OrderItem
                {
                    ProductName = "Mouse",
                    Quantity = 1,
                    UnitPrice = 600
                }
            }
        };
        customer1.Orders.Add(order1);
        customer2.Orders.Add(order2);
        customer3.Orders.Add(order3);

        context.Customers.Add(customer1);
        context.Customers.Add(customer2);
        context.Customers.Add(customer3);

        await context.SaveChangesAsync();
    }
}