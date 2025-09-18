using Auth.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Services
{
    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        private readonly ApplicationOptions _applicationOptions;

        public ConfigureJwtBearerOptions(IConfigurationManager<OpenIdConnectConfiguration> configurationManager,
                                         IOptions<ApplicationOptions> applicationOptions)
        {
            _configurationManager = configurationManager;
            _applicationOptions = applicationOptions.Value;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            if (name != null && name == JwtBearerDefaults.AuthenticationScheme)
            {
                options.ConfigurationManager = _configurationManager;
                options.MapInboundClaims = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = _applicationOptions.Issuer,
                    ValidAudience = _applicationOptions.Issuer,
                };
            }
        }
    }
}
