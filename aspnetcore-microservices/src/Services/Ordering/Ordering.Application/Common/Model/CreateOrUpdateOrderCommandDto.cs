﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Features.Commands
{
    public class CreateOrUpdateOrderCommandDto
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
    }
}