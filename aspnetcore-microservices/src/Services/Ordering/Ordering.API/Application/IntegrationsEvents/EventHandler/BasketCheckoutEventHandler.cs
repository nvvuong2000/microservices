using AutoMapper;
using EventBus.Message.IntegrationEvent.Event;
using MassTransit;
using Ordering.Application.Common.Features.Commands;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.API.Application.IntegrationsEvents.EventHandler
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public BasketCheckoutEventHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderRepository =orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var input = _mapper.Map<CreateOrUpdateOrderCommandDto>(context.Message);
            var order = _mapper.Map<Order>(input);
            var addedOrder = await _orderRepository.CreateOrder(order);
            await _orderRepository.SaveChangeAsync();
            //_logger.LogInformation($" Order {addedOrder.Id} is successfully created");
           //return new ApiSuccessResult<long>(addedOrder.Id);
        }
    }
}
