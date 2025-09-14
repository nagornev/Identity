using Auth.Application.Abstractions.Validators;
using Auth.Application.Converters;
using Auth.Application.DTOs;
using Auth.Security.Abstractions.Validators;
using System.Text;

namespace Auth.Security.Validators
{
    public class FingerprintValidator : IFingerprintValidator
    {
        private readonly IEd25519SignatureValidator _ed25519SignatureValidator;

        public FingerprintValidator(IEd25519SignatureValidator ed25519SignatureValidator)
        {
            _ed25519SignatureValidator = ed25519SignatureValidator;
        }

        public bool Validate(FingerprintValidationParameters parameters, string publicKey)
        {
            byte[] message = Encoding.UTF8.GetBytes(parameters.Message);
            byte[] encodedPublicKey = Base64UrlConverter.FromBase64Url(publicKey);
            byte[] encodedSignature = Base64UrlConverter.FromBase64Url(parameters.Signature);

            return _ed25519SignatureValidator.Validate(message, encodedPublicKey, encodedSignature);
        }
    }
}
