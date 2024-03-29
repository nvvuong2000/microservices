﻿using Constracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBaseAsync<Order, long, OrderContext>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext, IUnitOfWork<OrderContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) =>

            await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();

        public async Task<Order> CreateOrder(Order order)
        {
            var id = await CreateAsync(order);
            order.Id = id;
            return order;
        }

    }
}
