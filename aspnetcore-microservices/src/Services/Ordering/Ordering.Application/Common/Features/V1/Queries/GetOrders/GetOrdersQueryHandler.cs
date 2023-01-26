using AutoMapper;
using MediatR;
using Ordering.Application.Common.Features.V1.Queries.GetOrders;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Model;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Features.V1.Queries.GetOrders
{
    //public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    //{
    //    private readonly IMapper _mapper;
    //    private readonly IOrderRepository _repository;


    //    public GetOrdersQueryHandler(IMapper mapper, IOrderRepository repository)
    //    {
    //        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    //    }
    //    public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    //    {
    //        var orderEntities = await _repository.GetOrdersByUserName(request.UserName);
    //        var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

    //        return new ApiSuccessResult<List<OrderDto>>(orderList);
    //    }
    //}
}
