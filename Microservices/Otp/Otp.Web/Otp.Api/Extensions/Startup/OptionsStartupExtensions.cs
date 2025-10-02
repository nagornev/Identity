using Microsoft.Extensions.Options;
using Otp.Application.Options;
using Otp.Messaging.Options;

namespace Otp.Api.Extensions.Startup
{
    public static class OptionsStartupExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationOptions(configuration)
                           .AddInfrastructureOptions(configuration);
        }

        private static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton(Options.Create(configuration.GetSection(nameof(OneTimePasswordOptions))
                                                                     .Get<OneTimePasswordOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(SecretOptions))
                                                                     .Get<SecretOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(ApplicationOptions))
                                                                     .Get<ApplicationOptions>()!));
        }

        private static IServiceCollection AddInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton(Options.Create(configuration.GetSection(nameof(MessageBrokerOptions))
                                                                     .Get<MessageBrokerOptions>()!));
        }
    }
}
