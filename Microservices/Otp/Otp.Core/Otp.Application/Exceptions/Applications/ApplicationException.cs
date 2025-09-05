using OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Exceptions.Applications
{
    public abstract class ApplicationException : ResultException
    {
        public ApplicationException(int type, string message) 
            : base(type, message)
        {
        }
    }
}
