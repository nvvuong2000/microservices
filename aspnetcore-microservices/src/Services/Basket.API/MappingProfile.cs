using AutoMapper;
using Basket.API.Entities;
using EventBus.Message.IntegrationEvent.Event;

namespace Basket.API
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}
