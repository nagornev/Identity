using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class SessionClosedDomainEvent : DomainEvent
    {
        public SessionClosedDomainEvent(Guid aggregateId) 
            : base(aggregateId)
        {
        }
    }
}
