using Otp.Application.Abstractions.Factories;
using Otp.Application.Factories;

namespace Otp.Api.Extensions.Startup
{
    public static class FactoriesStartupExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationFactories();

        }

        private static IServiceCollection AddApplicationFactories(this IServiceCollection services)
        {
            return services.AddSingleton<IOneTimePasswordFactory, OneTimePasswordFactory>()
                           .AddSingleton<IUserFactory, UserFactory>();
        }
    }
}
