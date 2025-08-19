using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IFingerprintValidationService
    {
        void Validate(FingerprintValidationParameters parameters, string publicKey);
    }
}
