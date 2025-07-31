using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class EmailAddressChangeRequestService : IEmailAddressChangeRequestService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOtpClient _otpClient;

        public EmailAddressChangeRequestService(IUserQueryService userQueryService,
                                                IUnitOfWork unitOfWork,
                                                IOtpClient otpClient)
        {
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
            _otpClient = otpClient;
        }

        public async Task<string> RequestAsync(Guid userId, string emailAddress, CancellationToken cancellation)
        {
            if (await _userQueryService.IsUserAlreadyExistsAsync(emailAddress, cancellation))
                throw new UserInvalidEmailAddressApplicationException(emailAddress);

            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);
            user.ChangeEmailAddress(emailAddress);

            await _unitOfWork.SaveAsync(cancellation);

            return await _otpClient.CreateAsync(user.Id, OtpTags.ChangeEmailAddress, cancellation);
        }
    }
}
