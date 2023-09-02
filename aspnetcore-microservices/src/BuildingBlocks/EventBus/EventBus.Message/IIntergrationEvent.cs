using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message
{
    public interface IIntergrationEvent
    {
        DateTime CreationDate { get; }
        Guid Id { get; set; }
    }
}
