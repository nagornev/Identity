using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications
{
    public abstract class EmptyApplicationException : ApplicationException
    {
        public EmptyApplicationException(string message) 
            : base(ApplicationErrorTypes.Empty, message)
        {
        }
    }
}
