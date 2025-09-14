using Auth.Application.DTOs;

namespace Auth.Application.Features.Refresh
{
    public class RefreshCommand : ResultTRequest<TokenPair>
    {
        public RefreshCommand(string refreshToken,
                              string newPublicKey,
                              long timestamp,
                              string signature,
                              RequestContext requestContext)
        {
            RefreshToken = refreshToken;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
            RequestContext = requestContext;
        }

        public string RefreshToken { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }

        public RequestContext RequestContext { get; }
    }
}
