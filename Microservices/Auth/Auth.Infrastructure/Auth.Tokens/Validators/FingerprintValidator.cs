using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Validators;
using Auth.Application.DTOs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using System.Text;

namespace Auth.Tokens.Validators
{
    public class FingerprintValidator : IFingerprintValidator
    {
        private const int _publicKeyLength = 32; //public key length for Ed25519
        private const int _signatureLength = 64; //signature length for Ed25519

        private readonly IFingerprintFormatProvider _fingerprintFormatProvider;

        public FingerprintValidator(IFingerprintFormatProvider fingerprintFormatProvider)
        {
            _fingerprintFormatProvider = fingerprintFormatProvider;
        }

        public Task<bool> ValidateAsync(FingerprintValidationParameters parameters, string publicKey, CancellationToken cancellation = default)
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(_fingerprintFormatProvider.GetFormat(parameters.RefreshToken,
                                                                                             parameters.NewPublicKey,
                                                                                             parameters.Timestamp));
                byte[] encodedPublicKey = Convert.FromBase64String(publicKey);
                byte[] encodedSignature = Convert.FromBase64String(parameters.Signature);

                EnsurePublicKeyAndSignature(encodedPublicKey, encodedSignature);

                Ed25519PublicKeyParameters edPublicKey = new Ed25519PublicKeyParameters(encodedPublicKey, 0);
                Ed25519Signer edSignatureValidator = GetSignatureValidator(message, edPublicKey);

                return Task.FromResult(edSignatureValidator.VerifySignature(encodedSignature));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private void EnsurePublicKeyAndSignature(byte[] encodedPublicKey, byte[] encodedSignature)
        {
            if (encodedPublicKey.Length != _publicKeyLength)
                throw new ArgumentException("Invaid public key length.");

            if (encodedSignature.Length != _signatureLength)
                throw new ArgumentException("Invalid signature length.");
        }

        private Ed25519Signer GetSignatureValidator(byte[] message, Ed25519PublicKeyParameters key)
        {
            Ed25519Signer verifier = new Ed25519Signer();
            verifier.Init(false, key);
            verifier.BlockUpdate(message, 0, message.Length);

            return verifier;
        }
    }
}
