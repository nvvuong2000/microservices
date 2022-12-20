using ILogger = Serilog.ILogger;
using Customer.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistance
{
    public static class CustomerContextSeed
    {
        public static IHost SeedCustomerData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var customerContext = scope.ServiceProvider.GetService<CustomerContext>();
            customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

            CreateCustomer(customerContext, "customer1", "customer 1", "customer1 last name", "customer1@gmail.com").GetAwaiter().GetResult();
            CreateCustomer(customerContext, "customer2", "customer 2", "customer2 last name", "customer2@gmail.com").GetAwaiter().GetResult();
            return host;
        }

        private static async Task CreateCustomer(CustomerContext customerContext, string userName, string firstName, string lastName, string email)
        {
            var customer = await customerContext.Customer.SingleOrDefaultAsync(x => x.UserName.Equals(userName) || x.EmailAddress.Equals(email));
            if (customer == null)
            {
                var newCustomer = new Entities.Customer
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email
                };

                await customerContext.Customer.AddAsync(newCustomer);
                await customerContext.SaveChangesAsync();
            }

        }
    }
}
