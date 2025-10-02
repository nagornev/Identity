using Auth.Api.Services;
using Auth.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ScopeAuthorizationRequirement;

namespace Auth.Api.Extensions.Startup
{
    public static class AuthStartupExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            ApplicationOptions applicationOptions = configuration.GetSection(nameof(ApplicationOptions))
                                                                 .Get<ApplicationOptions>()!;

            return services.AddAuthentication(applicationOptions)
                           .AddAuthorization(applicationOptions);
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, ApplicationOptions applicationOptions)
        {

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer()
                           .Services
                           //configuration manager for getting JWK collection
                           .AddSingleton<IConfigurationManager<OpenIdConnectConfiguration>, JwksConfigurationManager>()
                           //configuration token validation parameters
                           .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services, ApplicationOptions applicationOptions)
        {
            return services.AddAuthorization(options =>
            {
                foreach (ApplicationOptions.Scope scope in applicationOptions.Roles.Basic.Scopes)
                {
                    options.AddPolicy(scope.Name, policy =>
                            policy.RequireScope(scope.Name));
                }

                foreach (ApplicationOptions.Scope scope in applicationOptions.Roles.Owner.Scopes)
                {
                    options.AddPolicy(scope.Name, policy =>
                            policy.RequireScope(scope.Name));
                }
            });
        }
    }
}
