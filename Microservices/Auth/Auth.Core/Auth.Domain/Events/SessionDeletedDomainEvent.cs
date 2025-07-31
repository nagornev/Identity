using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class SessionDeletedDomainEvent : DomainEvent
    {
        public SessionDeletedDomainEvent(Guid aggregateId) 
            : base(aggregateId)
        {
        }
    }
}
