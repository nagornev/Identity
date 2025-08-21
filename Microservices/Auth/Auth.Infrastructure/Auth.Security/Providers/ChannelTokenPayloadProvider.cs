using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Security.Consts;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Providers
{
    public class ChannelTokenPayloadProvider : IChannelTokenPayloadProvider
    {
        public ChannelTokenPayload GetPayload(IReadOnlyDictionary<string, string> claims)
        {
            return new ChannelTokenPayload(Guid.Parse(claims[JwtRegisteredClaimNames.Sub]),
                                           claims[ClaimNames.ChannelTag],
                                           claims[ClaimNames.Channel]);
        }
    }
}
