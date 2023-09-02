using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repository.Interface;
using EventBus.Message.IntegrationEvent.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
    
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IPublishEndpoint publishEndpoint, IMapper mapper,ILogger<BasketController> logger, IBasketRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasketByUserName(basketCheckout.UserName);
            if (basket == null) return NotFound();

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            _publishEndpoint.Publish(eventMessage);
            await _repository.DeleteBasketFromUserName(basketCheckout.UserName);
            return Accepted();
        }
    }
}