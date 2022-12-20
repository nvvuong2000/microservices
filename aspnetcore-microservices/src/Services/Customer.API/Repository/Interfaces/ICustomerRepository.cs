using Constracts.Common.Interfaces;
using Customer.API.Persistance;
namespace Customer.API.Repository.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerByUserNameAsync(string productNo);

        Task<IEnumerable<Entities.Customer>> GetCustomersAsync();

    }

}
