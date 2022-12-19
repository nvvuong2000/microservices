using Constracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Product.API.Persistance;
using Product.API.Repository.Interfaces;
using System.Linq.Expressions;

namespace Product.API.Repository
{
    public class ProductRepository: RepositoryBaseAsync<Entities.Product, long, ProductContext>, IProductRepository
    {
        
        public ProductRepository(ProductContext dbContext,IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }

        public async Task<IEnumerable<Entities.Product>> GetProducts() => await FindAll().ToListAsync();

        public Task<Entities.Product> GetProduct(long id) => GetByIdAsync(id);


        public Task<Entities.Product> GetProductByNo(string productNo) => FindByCondition(x=>x.No.Equals(productNo)).SingleOrDefaultAsync();

        public Task CreateProduct(Entities.Product product) => CreateAsync(product);
        

        public Task UpdateProduct(Entities.Product product) => UpdateAsync(product);

        public async Task DeleteProduct(long id)
        {
            var product = await GetProduct(id);
            if(product != null) DeleteAsync(product);
        }
    }
}
