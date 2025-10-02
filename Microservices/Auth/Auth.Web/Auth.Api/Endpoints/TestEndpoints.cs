using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Converters;
using Auth.Application.Features.Refresh;
using Carter;
using Hangfire.PostgreSql.Utils;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Endpoints
{
    public class TestEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("test");

            group.MapGet("auth", TestAuth)
                 .RequireAuthorization("edit:profile");

            group.MapGet("signature/keys", GetSignatureKeys);
            group.MapPost("signature/refresh", SignRefresh);
        }

        private static async Task<IResult> TestAuth(ClaimsPrincipal claimsPrincipal)
        {
            return Results.Ok(claimsPrincipal.Claims.Select(x => x.Value));
        }

        private static IResult GetSignatureKeys()
        {
            Ed25519KeyPairGenerator generator = new Ed25519KeyPairGenerator();
            generator.Init(new Ed25519KeyGenerationParameters(new SecureRandom()));

            AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();

            Ed25519PrivateKeyParameters privateKey = (Ed25519PrivateKeyParameters)keyPair.Private;
            Ed25519PublicKeyParameters publicKey = (Ed25519PublicKeyParameters)keyPair.Public;

            byte[] privateKeyBytes = privateKey.GetEncoded();
            byte[] publicKeyBytes = publicKey.GetEncoded();

            return Results.Ok(new
            {
                PrivateKey = Base64UrlConverter.ToBase64Url(privateKeyBytes),
                PublicKey = Base64UrlConverter.ToBase64Url(publicKeyBytes),
            });
        }

        private static IResult SignRefresh(SignRefreshContract contract, IFingerprintMessageProvider fingerprintMessageProvider, ITimeProvider timeProvider)
        {
            long timestamp = timeProvider.NowUnix();
            string messageToSign = fingerprintMessageProvider.GetMessage(contract.RefreshToken, contract.NewPublicKey, timestamp);

            byte[] encodedMessage = Encoding.UTF8.GetBytes(messageToSign);
            byte[] encodedSecret = Base64UrlConverter.FromBase64Url(contract.Secret);
            Ed25519PrivateKeyParameters pk = new Ed25519PrivateKeyParameters(encodedSecret, 0);
            Ed25519Signer edSignatureCreator = new Ed25519Signer();

            edSignatureCreator.Init(true, pk);
            edSignatureCreator.BlockUpdate(encodedMessage, 0, encodedMessage.Length);

            string signature = Base64UrlConverter.ToBase64Url(edSignatureCreator.GenerateSignature());

            return Results.Ok(new
            {
                contract.RefreshToken,
                contract.NewPublicKey,
                Timestamp = timestamp,
                Signature = signature,
            });
        }

        public class SignRefreshContract
        {
            public SignRefreshContract(string refreshToken,
                                       string newPublicKey,
                                       string secret)
            {
                RefreshToken = refreshToken;
                NewPublicKey = newPublicKey;
                Secret = secret;
            }

            public string RefreshToken { get; }

            public string NewPublicKey { get; }

            public string Secret { get; }
        }
    }
}
