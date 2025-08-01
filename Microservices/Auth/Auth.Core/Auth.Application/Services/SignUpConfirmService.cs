using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignUpConfirmService : ISignUpConfirmService
    {
        private readonly IEmailTokenValidator _emailTokenValidator;

        private readonly IEmailTokenMapper _emailTokenMapper;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public SignUpConfirmService(IEmailTokenValidator emailTokenValidator,
                                    IEmailTokenMapper emailTokenMapper,
                                    IUserQueryService userQueryService,
                                    IUnitOfWork unitOfWork)
        {
            _emailTokenValidator = emailTokenValidator;
            _emailTokenMapper = emailTokenMapper;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string emailToken, CancellationToken cancellation = default)
        {
            if (!await _emailTokenValidator.ValidateAsync(emailToken, cancellation))
                throw new EmailTokenInvalidApplicationException(emailToken);

            EmailToken token = await _emailTokenMapper.MapAsync(emailToken, cancellation);

            User user = await _userQueryService.GetUserByIdAsync(token.UserId, cancellation);
            user.Activate();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
