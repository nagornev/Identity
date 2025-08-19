using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInRequestService
    {
        Task<string> RequestAsync(string emailAddress, string password, string audience, string publicKey, RequestContext requestContext, CancellationToken cancellation = default);
    }
}
