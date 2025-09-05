using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers;
using Auth.Domain.Aggregates;

namespace Auth.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly ISaltFactory _saltFactory;

        private readonly IPasswordHashProvider _passwordHashProvider;

        private readonly ITimeProvider _timeProvider;

        public UserFactory(ISaltFactory saltFactory,
                           IPasswordHashProvider hashProvider,
                           ITimeProvider timeProvider)
        {
            _saltFactory = saltFactory;
            _passwordHashProvider = hashProvider;
            _timeProvider = timeProvider;
        }

        public User Create(string email, string personName, string password)
        {
            string passwordSalt = _saltFactory.Create();

            return User.Create(email,
                               personName,
                               _passwordHashProvider.Hash(password, passwordSalt),
                               passwordSalt,
                               _timeProvider.NowUnix());
        }
    }
}
