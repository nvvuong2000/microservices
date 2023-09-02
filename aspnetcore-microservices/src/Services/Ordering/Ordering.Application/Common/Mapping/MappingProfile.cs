using AutoMapper;
using EventBus.Message.IntegrationEvent.Event;
using Ordering.Application.Common.Features.Commands;
using Ordering.Application.Common.Model;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<BasketCheckoutEvent, CreateOrUpdateOrderCommandDto>();
            CreateMap<CreateOrUpdateOrderCommandDto, Order>();
        }
    }
}
