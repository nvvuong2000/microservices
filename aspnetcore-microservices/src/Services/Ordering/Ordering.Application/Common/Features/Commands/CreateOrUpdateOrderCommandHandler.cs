using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Features.Commands
{
    public class CreateOrUpdateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;


        public CreateOrUpdateOrderCommandHandler(IMapper mapper, IOrderRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            var result = await _repository.CreateOrder(order);

            return new ApiSuccessResult<long>(result.Id);
        }

    }
}