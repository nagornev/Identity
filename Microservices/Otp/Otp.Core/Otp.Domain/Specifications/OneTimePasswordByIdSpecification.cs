using DDD.Specifications;
using Otp.Domain.Aggregates;
using System.Linq.Expressions;

namespace Otp.Domain.Specifications
{
    public class OneTimePasswordByIdSpecification : Specification<OneTimePassword>
    {
        private readonly Guid _id;

        public OneTimePasswordByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<OneTimePassword, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
