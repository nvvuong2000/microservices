using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private readonly ILogger<OrderContextSeed> _logger;
        private readonly OrderContext _context;


        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", TotalPrice = 350 }
            };
        }
    }
    //public static OrderContextSeed(ILogger<OrderContextSeed> logger, OrderContext context)
    //{
    //    _logger = logger;
    //    _context = context;
    //}

    //public async Task InitializeAsync()
    //{
    //    try
    //    {
    //        if (_context.Database.IsSqlServer())
    //        {
    //            await _context.Database.MigrateAsync();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "An error occurred while initialing the database");
    //        throw;
    //    }
    //}

    //public static async Task SeedAsync()
    //{
    //    if (!_context.Orders.Any())
    //    {
    //        await _context.Orders.AddRangeAsync(new Order
    //        {
    //            UserName = "customer1",
    //            FirstName = "customer1",
    //            LastName = "customer",
    //            EmailAddress = "nguyenvuongs2000@gamil.com",
    //            ShippingAddress = "ABc",
    //            InvoiceAddress = "abc",
    //            TotalPrice = 250
    //        });
    //        await _context.SaveChangesAsync();
    //    }
    //}

}
