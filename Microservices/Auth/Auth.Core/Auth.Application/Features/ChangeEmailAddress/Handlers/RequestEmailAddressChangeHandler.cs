using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangeEmailAddress.Commands;

namespace Auth.Application.Features.ChangeEmailAddress.Handlers
{
    public class RequestEmailAddressChangeHandler : ResultTRequestHandler<RequestEmailAddressChangeCommand, string>
    {
        private readonly IEmailAddressChangeRequestService _emailAddressChangeRequestService;

        public RequestEmailAddressChangeHandler(IEmailAddressChangeRequestService emailAddressChangeRequestService)
        {
            _emailAddressChangeRequestService = emailAddressChangeRequestService;
        }

        public override async Task<string> HandleAsync(RequestEmailAddressChangeCommand request, CancellationToken cancellation)
        {
            return await _emailAddressChangeRequestService.RequestAsync(request.UserId, request.EmailAddress, cancellation);
        }
    }
}
