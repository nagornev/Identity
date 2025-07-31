using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using System.Security.Cryptography;

namespace Auth.Tokens.Factories
{
    public abstract class RsaKeysFactory
    {
        private const int _keySize = 2048;

        private readonly ITimeProvider _timeProvider;

        public RsaKeysFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public KeyPairDto Create(TimeSpan timeToLive)
        {
            Guid kid = Guid.NewGuid();
            byte[] publicKey;
            byte[] privateKey;
            long createdAt = _timeProvider.NowUnix();
            long expiresAt = createdAt + timeToLive.Seconds;

            using (RSA rsa = RSA.Create(_keySize))
            {
                publicKey = rsa.ExportRSAPublicKey();
                privateKey = rsa.ExportRSAPrivateKey();
            }

            return new KeyPairDto(kid, publicKey, privateKey, createdAt, expiresAt);
        }

        public RSA FromPrivateKey(byte[] key)
        {
            RSA rsa = RSA.Create();

            rsa.ImportRSAPrivateKey(new ReadOnlySpan<byte>(key), out var _);

            return rsa;
        }

        public RSA FromPublicKey(byte[] key)
        {
            RSA rsa = RSA.Create();

            rsa.ImportRSAPublicKey(new ReadOnlySpan<byte>(key), out var _);

            return rsa;
        }

    }
}
