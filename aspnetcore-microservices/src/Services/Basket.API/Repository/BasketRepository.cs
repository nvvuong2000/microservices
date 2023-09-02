using Basket.API.Entities;
using Basket.API.Repository.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheServies;
        public BasketRepository(IDistributedCache redisCacheServies)
        {
            _redisCacheServies = redisCacheServies;

        }
        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisCacheServies.RemoveAsync(userName);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            var basket = await _redisCacheServies.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<Cart?>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            if (options != null)
            {
                await _redisCacheServies.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart), options);
            }
            else
            {
                await _redisCacheServies.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            }
            return await GetBasketByUserName(cart.UserName);
        }
    }
}
