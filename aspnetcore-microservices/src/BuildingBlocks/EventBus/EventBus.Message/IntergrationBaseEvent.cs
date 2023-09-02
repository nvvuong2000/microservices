using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message
{
    public record IntergrationBaseEvent() : IIntergrationEvent
    {
        public DateTime CreationDate { get; } = DateTime.UtcNow;
        public Guid Id { get;set; }
    }
}
