using Auth.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ScopeAuthorizationRequirement;

namespace Auth.Api.Extensions.Startup
{
    public static class AuthStartupExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            ApplicationOptions applicationOptions = configuration.GetSection(nameof(ApplicationOptions))
                                                                 .Get<ApplicationOptions>()!;

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer(options =>
                           {
                               options.MetadataAddress = "https://localhost:5000/jwks";

                               options.TokenValidationParameters = new TokenValidationParameters
                               {
                                   ValidateIssuer = true,
                                   ValidIssuer = applicationOptions.Issuer,

                                   ValidateAudience = true,
                                   ValidAudience = applicationOptions.Issuer,

                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true
                               };
                           })
                           .Services
                           .AddAuthorization(options =>
                           {
                               options.AddPolicy("read:profile", policy =>
                                policy.RequireScope("read:profile"));

                               options.AddPolicy("edit:profile", policy =>
                                policy.RequireScope("edit:profile"));
                           });
        }
    }
}
