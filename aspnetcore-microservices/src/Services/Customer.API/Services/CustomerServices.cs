using Customer.API.Repository.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _repository;

        public CustomerServices(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var customer = await _repository.GetCustomerByUserNameAsync(userName);
            return customer == null ? Results.NotFound() : Results.Ok(customer);
        }

        public async Task<IResult> GetCustomersAsync() =>
            Results.Ok(await _repository.GetCustomersAsync());
    }
}
