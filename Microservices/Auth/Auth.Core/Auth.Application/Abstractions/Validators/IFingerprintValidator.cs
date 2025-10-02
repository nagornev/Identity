using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Validators
{
    public interface IFingerprintValidator
    {
        bool Validate(FingerprintValidationParameters parameters, string publicKey);
    }
}
