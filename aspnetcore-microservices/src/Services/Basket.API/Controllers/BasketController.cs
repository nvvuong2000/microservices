using Basket.API.Entities;
using Basket.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
    
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketRepository _repository;

        public BasketController(ILogger<BasketController> logger, IBasketRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get([Required] string username)
        {
            var result = await _repository.GetBasketByUserName(username);
            return Ok(result == null ? new Cart() : result);
        }

        [HttpPost(Name ="UpdateBasket")]
        public async Task<IActionResult> UpdateBasket([FromBody] Cart cart)
        {
            var option = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
                 .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        
            var result = await _repository.UpdateBasket(cart, option);
            return Ok(result);
        }

        [HttpDelete("{username}",Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket([Required] string username)
        {
            var result = await _repository.DeleteBasketFromUserName(username);
            return Ok(result);
        }
    }
}