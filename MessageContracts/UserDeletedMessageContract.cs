using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class UserDeletedMessageContract : IMessageContract
    {
        public UserDeletedMessageContract(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
