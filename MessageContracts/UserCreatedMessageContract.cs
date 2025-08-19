using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class UserCreatedMessageContract : IMessageContract
    {
        public UserCreatedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
