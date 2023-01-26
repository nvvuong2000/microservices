using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Features.V1.Queries.GetOrders;
using Ordering.Application.Common.Model;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderControler : ControllerBase
    {
        private readonly IMediator _mediator;
        private IServiceScopeFactory _serviceScopeFactory;
        public OrderControler(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _serviceScopeFactory = serviceScopeFactory;
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
        }


        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var result = mediator.Send(new GetOrdersQuery(username));
                return Ok(result);
            }
               
            

        }
    }
}