using Auth.Domain.Aggregates;
using DDD.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications
{
    public class SessionByValidParametersSpecification : Specification<Session>
    {
        private readonly long _timestamp;

        public SessionByValidParametersSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<Session, bool>> ToExpression()
        {
            return x => x.Activated &&
                       !x.Closed &&
                       !x.Revoked &&
                       !x.Deleted &&
                        x.ExpiresAt > _timestamp;
        }
    }
}
