using DDD.Specifications;
using Otp.Domain.Aggregates;
using System.Linq.Expressions;

namespace Otp.Domain.Specifications
{
    public class OneTimePasswordByExpiredBeforeSpecification : Specification<OneTimePassword>
    {
        private readonly long _timestamp;

        public OneTimePasswordByExpiredBeforeSpecification(long timestamp)
        {
            _timestamp = timestamp;
        }

        public override Expression<Func<OneTimePassword, bool>> ToExpression()
        {
            return x => x.ExpiresAt < _timestamp;
        }
    }
}
