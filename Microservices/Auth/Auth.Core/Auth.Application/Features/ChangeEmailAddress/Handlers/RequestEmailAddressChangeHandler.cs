using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Features.ChangeEmailAddress.Commands;

namespace Auth.Application.Features.ChangeEmailAddress.Handlers
{
    public class RequestEmailAddressChangeHandler : ResultTRequestHandler<RequestEmailAddressChangeCommand, Otp>
    {
        private readonly IEmailAddressChangeRequestService _emailAddressChangeRequestService;

        public RequestEmailAddressChangeHandler(IEmailAddressChangeRequestService emailAddressChangeRequestService)
        {
            _emailAddressChangeRequestService = emailAddressChangeRequestService;
        }

        public override async Task<Otp> HandleAsync(RequestEmailAddressChangeCommand request, CancellationToken cancellation)
        {
            return await _emailAddressChangeRequestService.RequestAsync(request.UserId, request.EmailAddress, cancellation);
        }
    }
}
