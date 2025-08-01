using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Tokens;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class EmailAddressUpdateService : IEmailAddressUpdateService
    {
        private readonly IEmailTokenValidator _emailTokenValidator;

        private readonly IEmailTokenMapper _emailTokenMapper;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public EmailAddressUpdateService(IEmailTokenValidator emailTokenValidator,
                                         IEmailTokenMapper emailTokenMapper,
                                         IUserQueryService userQueryService,
                                         IUnitOfWork unitOfWork)
        {
            _emailTokenValidator = emailTokenValidator;
            _emailTokenMapper = emailTokenMapper;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateAsync(string emailToken, CancellationToken cancellation = default)
        {
            if (!await _emailTokenValidator.ValidateAsync(emailToken, cancellation))
                throw new EmailTokenInvalidApplicationException(emailToken);

            EmailTokenDto emailTokenDto = await _emailTokenMapper.MapAsync(emailToken, cancellation);

            User user = await _userQueryService.GetUserByIdAsync(emailTokenDto.UserId, cancellation);
            user.UpdateEmailAddress();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
