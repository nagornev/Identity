using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignUpConfirmService : ISignUpConfirmService
    {
        private readonly IEmailTokenPayloadMapper _emailTokenMapper;

        private readonly IEmailKeyStorage _emailKeyStorage;

        private readonly IEmailTokenValidator _emailTokenValidator;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

                                   
        public SignUpConfirmService(IEmailTokenPayloadMapper emailTokenMapper,
                                    IEmailKeyStorage emailKeyStorage,
                                    IEmailTokenValidator emailTokenValidator,
                                    IUserQueryService userQueryService,
                                    IUnitOfWork unitOfWork)
        {
            _emailTokenValidator = emailTokenValidator;
            _emailTokenMapper = emailTokenMapper;
            _emailKeyStorage = emailKeyStorage;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string emailToken, CancellationToken cancellation = default)
        {
            EmailTokenPayload emailTokenPayload = await _emailTokenMapper.MapAsync(emailToken, cancellation);
            KeyPair emailValidationKey = await _emailKeyStorage.GetKeyPairAsync(emailTokenPayload.Kid, cancellation);

            if (!await _emailTokenValidator.ValidateAsync(emailToken, emailValidationKey, cancellation))
                throw new EmailTokenInvalidApplicationException(emailToken);

            User user = await _userQueryService.GetUserByIdAsync(emailTokenPayload.UserId, cancellation);
            user.Activate();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
