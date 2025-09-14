using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public record OneTimePasswordResendedMessageContract(Guid OneTimePasswordId)
    {
    }
}
