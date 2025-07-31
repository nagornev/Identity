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
    public class SessionUserIdSpecification : Specification<Session>
    {
        private readonly Guid _userId;

        public SessionUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Session, bool>> ToExpression()
        {
            return (session) => session.UserId == _userId;
        }
    }
}
