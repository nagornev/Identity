using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class FingerprintValidationService : IFingerprintValidationService
    {
        private readonly IFingerprintValidator _fingerprintValidator;

        public FingerprintValidationService(IFingerprintValidator fingerprintValidator)
        {
            _fingerprintValidator = fingerprintValidator;
        }

        public void Validate(FingerprintValidationParameters parameters, string publicKey)
        {
            if (!_fingerprintValidator.Validate(parameters, publicKey))
                throw new FingerprintInvalidApplicationException();
        }
    }
}
