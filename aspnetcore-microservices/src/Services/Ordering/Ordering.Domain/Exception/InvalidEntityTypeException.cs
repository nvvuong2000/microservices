using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exception
{
    public class InvalidEntityTypeException: ApplicationException
    {
        public InvalidEntityTypeException(string entity, string type) : base($"Entity \"{entity}\" ({type} was not found")
        {

        }
    }
}
