using AutoMapper;
using MediatR;
using Ordering.Application.Common.Mapping;
using Ordering.Domain.Entities;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Features.Commands
{
    public class CreateOrderCommand : CreateOrUpdateOrderCommand, IRequest<ApiResult<long>>, IMapFrom<Order>
    {
        public string UserName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, Order>();
        }
    }
}