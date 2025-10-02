using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class UserCreateService : IUserCreateService
    {
        private readonly IUserFactory _userFactory;

        private readonly IUserQueryService _userQueryService;

        private readonly IUserInitializeService _userInitializeService;

        public UserCreateService(IUserFactory userFactory,
                                 IUserQueryService userQueryService,
                                 IUserInitializeService userInitializeService)
        {
            _userFactory = userFactory;
            _userQueryService = userQueryService;
            _userInitializeService = userInitializeService;
        }

        public async Task<User> CreateAsync(string emailAddress, string personName, string password, CancellationToken cancellation = default)
        {
            if (await _userQueryService.IsUserAlreadyExistsAsync(emailAddress, cancellation))
                throw new UserAlreadyExistsApplicationException(emailAddress);

            User user = _userFactory.Create(emailAddress, personName, password);

            await _userInitializeService.Initialize(user, cancellation);

            return user;
        }
    }
}
