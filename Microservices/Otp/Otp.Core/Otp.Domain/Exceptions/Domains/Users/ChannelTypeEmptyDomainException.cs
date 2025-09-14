using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.Users
{
    public class ChannelTypeEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The channel type can`t be empty.";

        public ChannelTypeEmptyDomainException() 
            : base(_message)
        {
        }
    }
}
