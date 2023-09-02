﻿using Ordering.Application.Common.Mapping;
using Ordering.Domain.Entities;
using Ordering.Domain.Enum;

namespace Ordering.Application.Common.Model
{
    public class OrderDto : IMapFrom<Order>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
        public EOrderStatus Status { get; set; }
    }
}
