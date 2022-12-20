using Constracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Customer.API.Persistance;
using Customer.API.Repository.Interfaces;
using System.Linq.Expressions;

namespace Customer.API.Repository
{
    public class CustomerRepository: RepositoryBaseAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
    {

        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }

        //public async Task CreateCustomer(Entities.Customer customer) => await CreateAsync(customer);

        //public async Task DeleteCustomer(Entities.Customer customer)
        //{
        //    await DeleteAsync(customer);
        //}

        public async Task<Entities.Customer> GetCustomerByUserNameAsync(string userName) =>
            await FindByCondition(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();

        public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync()
        {
            var abc = await FindAll().ToListAsync();
            return await FindAll().ToListAsync();
        }

       // public async Task UpdateCustomer(Entities.Customer customer) => await UpdateAsync(customer);


        //public async Task<IEnumerable<Entities.Customer>> GetProducts() => await FindAll().ToListAsync();

        //public Task<Entities.Customer> GetProduct(long id) => GetByIdAsync(id);


        //public Task<Entities.Customer> GetProductByNo(string productNo) => FindByCondition(x=>x.No.Equals(productNo)).SingleOrDefaultAsync();

        //public Task CreateProduct(Entities.Customer product) => CreateAsync(product);


        //public Task UpdateProduct(Entities.Customer product) => UpdateAsync(product);

        //public async Task DeleteProduct(long id)
        //{
        //    var product = await GetProduct(id);
        //    if(product != null) DeleteAsync(product);
        //}
    }
}
