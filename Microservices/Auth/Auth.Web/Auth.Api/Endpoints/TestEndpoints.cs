using Auth.Application.Abstractions.Providers;
using Auth.Application.Converters;
using Auth.Application.Features.Refresh;
using Auth.Application.Features.SignIn.Queries;
using Carter;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math.EC.Rfc8032;
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
                 .RequireAuthorization("read:profile");

            group.MapPost("sign/refresh", SignRefresh);
        }

        private static async Task<IResult> TestAuth(ClaimsPrincipal claimsPrincipal)
        {
            return Results.Ok(claimsPrincipal.Claims.Select(x=> x.Value));
        }


        private static async Task<IResult> SignRefresh(SignRefreshContract contract, IFingerprintMessageProvider fingerprintMessageProvider, ITimeProvider timeProvider)
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

            return Results.Ok(new RefreshCommand(contract.RefreshToken, contract.NewPublicKey, timestamp, signature, string.Empty, string.Empty));

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
