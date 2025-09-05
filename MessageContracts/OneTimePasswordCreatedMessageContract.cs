using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class OneTimePasswordCreatedMessageContract : IMessageContract
    {
        public OneTimePasswordCreatedMessageContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
