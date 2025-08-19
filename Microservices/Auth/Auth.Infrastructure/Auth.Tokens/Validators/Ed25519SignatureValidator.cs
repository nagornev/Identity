using Auth.Security.Abstractions.Validators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Auth.Security.Validators
{
    public class Ed25519SignatureValidator : IEd25519SignatureValidator
    {
        private const int _publicKeyLength = 32; //public key length for Ed25519
        private const int _signatureLength = 64; //signature length for Ed25519

        public bool Validate(byte[] data, byte[] publicKey, byte[] signature)
        {
            try
            {
                EnsurePublicKeyAndSignature(publicKey, signature);

                Ed25519PublicKeyParameters edPublicKey = new Ed25519PublicKeyParameters(publicKey, 0);
                Ed25519Signer edSignatureValidator = GetSignatureValidator(data, edPublicKey);

                return edSignatureValidator.VerifySignature(signature);
            }
            catch
            {
                return false;
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
