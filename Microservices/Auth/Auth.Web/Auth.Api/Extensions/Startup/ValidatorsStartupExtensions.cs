using Auth.Application.Abstractions.Validators;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.Validators;
using Auth.Security.Abstractions.Validators;
using Auth.Security.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Auth.Api.Extensions.Startup
{
    public static class ValidatorsStartupExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationValidators()
                           .AddInfrastructureValidators()
                           .AddContractValidators();
        }

        private static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            return services.AddSingleton<IPasswordValidator, PasswordValidator>()
                           .AddSingleton<ISessionValidator, SessionValidator>()
                           .AddSingleton<IUserValidator, UserValidator>()
                           .AddSingleton<IWindowValidator, WindowValidator>();
        }

        private static IServiceCollection AddInfrastructureValidators(this IServiceCollection services)
        {
            return services.AddSingleton<IRefreshTokenValidator, RefreshTokenValidator>()
                           .AddSingleton<IChannelTokenValidator, ChannelTokenValidator>()
                           .AddSingleton<IFingerprintValidator, FingerprintValidator>()

                           //Infastructure only
                           .AddSingleton<IJwtSignatureValidator, JwtSignatureValidator>()
                           .AddSingleton<IEd25519SignatureValidator, Ed25519SignatureValidator>();
        }

        public static IServiceCollection AddContractValidators(this IServiceCollection services)
        {
            return services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
                           .AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
