using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class OneTimePasswordNotificationMessageContract : IMessageContract
    {
        public OneTimePasswordNotificationMessageContract(Guid subject,
                                                          string oneTimePasswordValue)
        {
            Subject = subject;
            OneTimePasswordValue = oneTimePasswordValue;
        }

        public Guid Subject { get; }

        public string OneTimePasswordValue { get; }
    }
}
