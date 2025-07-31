using Auth.Domain.Aggregates;
using Auth.Domain.ValueObjects;
using DDD.Specifications;
using System.Linq.Expressions;

namespace Auth.Domain.Specifications
{
    public class UserEmailSpecification : Specification<User>
    {
        private readonly EmailAddress _emailAddress;

        public UserEmailSpecification(string email)
        {
            _emailAddress = EmailAddress.Create(email);
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Profile.EmailAddress == _emailAddress;
        }
    }
}
