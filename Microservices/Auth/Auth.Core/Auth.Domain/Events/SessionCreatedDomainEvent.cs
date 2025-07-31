using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class SessionCreatedDomainEvent : DomainEvent
    {
        public SessionCreatedDomainEvent(Guid aggregateId,
                                         Guid kid)
            : base(aggregateId)
        {
            Kid = kid;
        }

        public Guid Kid { get; }
    }
}
