using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class UserActivatedMessageContract : IMessageContract
    {
        public UserActivatedMessageContract(Guid userId,
                                            string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }

        public Guid UserId { get; }

        public string EmailAddress { get; }
    }
}
