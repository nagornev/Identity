using FluentValidation;
using FluentValidation.AspNetCore;

namespace Otp.Api.Extensions.Startup
{
    public static class ValidatorsStartupExtensions
    {

        public static IServiceCollection AddValidators(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddContractValidators();
        }

        private static IServiceCollection AddContractValidators(this IServiceCollection services)
        {
            return services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
                           .AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
