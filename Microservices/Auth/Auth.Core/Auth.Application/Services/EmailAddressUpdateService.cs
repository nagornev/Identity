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
    public class EmailAddressUpdateService : IEmailAddressUpdateService
    {
        private readonly IEmailTokenPayloadMapper _emailTokenMapper;

        private readonly IEmailKeyStorage _emailKeyStorage;

        private readonly IEmailTokenValidator _emailTokenValidator;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

                                         
        public EmailAddressUpdateService(IEmailTokenPayloadMapper emailTokenMapper,
                                         IEmailKeyStorage emailKeyStorage,
                                         IEmailTokenValidator emailTokenValidator,
                                         IUserQueryService userQueryService,
                                         IUnitOfWork unitOfWork)
        {
            _emailTokenMapper = emailTokenMapper;
            _emailKeyStorage = emailKeyStorage;
            _emailTokenValidator = emailTokenValidator;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateAsync(string emailToken, CancellationToken cancellation = default)
        {
            EmailTokenPayload emailTokenPayload = await _emailTokenMapper.MapAsync(emailToken, cancellation);
            KeyPair emailValidationKey = await _emailKeyStorage.GetKeyPairAsync(emailTokenPayload.Kid, cancellation);

            if (!await _emailTokenValidator.ValidateAsync(emailToken, emailValidationKey, cancellation))
                throw new EmailTokenInvalidApplicationException(emailToken);

            User user = await _userQueryService.GetUserByIdAsync(emailTokenPayload.UserId, cancellation);
            user.UpdateEmailAddress();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
