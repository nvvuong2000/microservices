﻿using AutoMapper;
using Ordering.Application.Common.Mapping;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Features.Commands
{
    public class CreateOrUpdateOrderCommand : IMapFrom<Order>
    {
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrUpdateOrderCommand, Order>();
        }
    }
}
