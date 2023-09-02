using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Model;
using Shared.SeedWork;

namespace Ordering.Application.Common.Features.Query
{
    public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>
    {
        public string UserName { get; private set; }
        public GetOrdersQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
