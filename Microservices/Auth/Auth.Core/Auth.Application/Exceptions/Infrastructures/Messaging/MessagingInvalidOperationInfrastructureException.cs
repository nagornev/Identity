using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Infrastructures.Messaging
{
    public class MessagingInvalidOperationInfrastructureException : InvalidInfrastructureException
    {
        public MessagingInvalidOperationInfrastructureException(string message, Exception? inner = null) 
            : base(message, inner)
        {
        }
    }
}
