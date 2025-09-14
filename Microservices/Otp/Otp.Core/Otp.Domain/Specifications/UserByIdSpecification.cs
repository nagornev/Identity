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
    public class UserByIdSpecification : Specification<User>
    {
        private readonly Guid _id;

        public UserByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
