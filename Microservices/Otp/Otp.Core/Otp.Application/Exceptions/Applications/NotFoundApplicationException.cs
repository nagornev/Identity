using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Exceptions.Applications
{
    public abstract class NotFoundApplicationException : ApplicationException
    {
        protected NotFoundApplicationException(string message) 
            : base(ApplicationErrorTypes.NotFound, message)
        {
        }
    }
}
