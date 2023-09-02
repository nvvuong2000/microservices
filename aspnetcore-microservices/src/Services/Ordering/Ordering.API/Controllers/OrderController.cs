using AutoMapper;
using Constracts.Message;
using Constracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Features.Commands;
using Ordering.Application.Common.Features.Query;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Model;
using Ordering.Domain.Entities;
using Shared.SeedWork;
using Shared.Services.Mail;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderControler : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailSevices<MailRequest> _emailService;
        private readonly IMessageProducer _messageProducer;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderControler(IMediator mediator, IEmailSevices<MailRequest> emailService, IMessageProducer messageProducer, IMapper mapper, IOrderRepository orderRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _emailService = emailService;
            _messageProducer = messageProducer;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
            public const string CreateOrder = nameof(CreateOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string DeleteOrder = nameof(DeleteOrder);

        }


        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
        {

            var result = await _mediator.Send(new GetOrdersQuery(username));
            return Ok(result);

        }

        #region CRUD
        [HttpGet]

        public async Task<IActionResult> TestEmail()
        {

            var message = new MailRequest
            {
                Subject = "Test",
                Body = "hello",
                ToAddresses = new List<string>(new string[] { "nguyenquynhs1945@gmail.com" }),
            };
            await _emailService.SendEmailRequest(message);
            return Ok();
        }


        [HttpPost(Name = RouteNames.CreateOrder)]
        [ProducesResponseType(typeof(ApiResult<long>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ApiResult<long>>> CreateOrder([FromBody]CreateOrderCommand orderDto)
        {
            var result = await _mediator.Send(orderDto);
            //var order = _mapper.Map<Order>(orderDto);
            //var addedOrder = await _orderRepository.CreateOrder(order);
            //await _orderRepository.SaveChangeAsync();
            //var result = _mapper.Map<OrderDto>(addedOrder);
            //_messageProducer.SendMessage(result);

            return Ok(result);

        }
        #endregion
    }
}