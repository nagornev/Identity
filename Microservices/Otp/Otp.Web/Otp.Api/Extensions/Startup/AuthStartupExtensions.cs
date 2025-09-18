using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Otp.Api.Extensions.Startup;
using Otp.Api.Services;
using Otp.Application.Options;
using ScopeAuthorizationRequirement;

namespace Otp.Api.Extensions.Startup
{
    public static class AuthStartupExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication()
                           .AddAuthorization();
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer()
                           .Services
                           //configuration manager for getting JWK collection
                           .AddSingleton<IConfigurationManager<OpenIdConnectConfiguration>, JwksConfigurationManager>()
                           //configuration token validation parameters
                           .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy("read:scheduler", policy =>
                        policy.RequireScope("read:scheduler"));
            });
        }
    }
}
