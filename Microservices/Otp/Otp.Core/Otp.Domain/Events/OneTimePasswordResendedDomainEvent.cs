using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Events
{
    public class OneTimePasswordResendedDomainEvent : DomainEvent
    {
        public OneTimePasswordResendedDomainEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
