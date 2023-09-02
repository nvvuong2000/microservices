using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repository.Interface
{
    public interface IBasketRepository
    {
        Task<Cart?> GetBasketByUserName(string UserName);
        Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null);
        Task<bool> DeleteBasketFromUserName(string UserName);

    }
}
