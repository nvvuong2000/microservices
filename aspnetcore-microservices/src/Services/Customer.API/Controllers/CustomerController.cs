using Customer.API.Repository.Interfaces;
using Customer.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers
{
    public static class CustomerController
    {
        public static void MapCustomerAPI(this WebApplication app)
        {
            app.MapGet("/", () => "Customer API");
            app.MapGet("/api/customers", async (ICustomerServices customerServices) => await customerServices.GetCustomersAsync());
            app.MapGet("/api/customers/{username}", async (string username, ICustomerServices customerServices) => await customerServices.GetCustomerByUserNameAsync(username));

            app.MapPost("/api/customers/", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            {
                await customerRepository.CreateAsync(customer);
                await customerRepository.SaveChangeAsync();

            });

            app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
            {
                var customer = await customerRepository.FindByCondition(x => x.UserName.Equals(id)).SingleOrDefaultAsync();
                if (customer != null)
                {
                    await customerRepository.DeleteAsync(customer);
                    await customerRepository.SaveChangeAsync();
                }

                return customer == null ? Results.NotFound() : Results.NoContent();
            });

            app.MapPut("/api/customers/{id}", async (int id, Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            {
                var exitstingCustomer = await customerRepository.FindByCondition(x => x.UserName.Equals(id)).SingleOrDefaultAsync();
                if (customer != null)
                {
                    await customerRepository.UpdateAsync(customer);
                    await customerRepository.SaveChangeAsync();
                }

                return customer == null ? Results.NotFound() : Results.NoContent();
            });
        }
    }
}
