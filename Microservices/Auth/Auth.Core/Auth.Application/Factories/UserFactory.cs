using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers;
using Auth.Domain.Aggregates;

namespace Auth.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IPasswordHashProvider _passwordHashProvider;

        private readonly ITimeProvider _timeProvider;

        public UserFactory(IPasswordHashProvider hashProvider,
                           ITimeProvider timeProvider)
        {
            _passwordHashProvider = hashProvider;
            _timeProvider = timeProvider;
        }

        public User Create(string email, string personName, string password)
        {
            return User.Create(email,
                               personName,
                               _passwordHashProvider.Hash(password),
                               _timeProvider.NowUnix());
        }
    }
}
