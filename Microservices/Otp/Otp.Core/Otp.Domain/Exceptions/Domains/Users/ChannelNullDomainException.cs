using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.Users
{
    public class ChannelNullDomainException : NullDomainException
    {
        private const string _message = "The channel can`t be null.";

        public ChannelNullDomainException() 
            : base(_message)
        {
        }
    }
}
