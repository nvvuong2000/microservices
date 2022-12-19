using Constracts.Common.Interfaces;
using Product.API.Persistance;
namespace Product.API.Repository.Interfaces
{
    public interface IProductRepository: IRepositoryBaseAsync<Entities.Product, long, ProductContext> 
    {
        Task<IEnumerable<Entities.Product>> GetProducts();
        Task<Entities.Product> GetProduct(long id);
        Task<Entities.Product> GetProductByNo(string productNo);
        Task CreateProduct(Entities.Product product);
        Task UpdateProduct(Entities.Product product);
        Task DeleteProduct(long id);
    }

}
