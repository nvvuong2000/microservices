using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Enum
{
    public enum EOrderStatus
    {
        New = 1,
        Pending,
        Paid,
        Shipping,
        Fulfilled
    }
}
