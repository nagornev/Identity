using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class OtpValidationService : IOtpValidationService
    {
        private readonly IOtpClient _otpClient;

        public OtpValidationService(IOtpClient otpClient)
        {
            _otpClient = otpClient;
        }

        public async Task<OtpContent> ValidateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default)
        {
            OtpValidation otpValidation = await _otpClient.ValidateAsync(otpToken, otp, tag, cancellation);

            if (!otpValidation.IsValid)
                throw new OtpInvalidApplicationException();

            return new OtpContent(otpValidation.Subject, otpValidation.Payload);
        }
    }
}
