using DDD.Specifications;
using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
