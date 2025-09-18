using Otp.Application.Abstractions.Providers;
using Otp.Application.Providers;
using Otp.Messaging.Abstractions.Providers;
using Otp.Messaging.Providers;
using TimeProvider = Otp.Application.Providers.TimeProvider;

namespace Otp.Api.Extensions.Startup
{
    public static class ProvidersStartupExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationProviders()
                           .AddInfrastructureProviders();
        }

        private static IServiceCollection AddApplicationProviders(this IServiceCollection services)
        {
            return services.AddSingleton<ITimeProvider, TimeProvider>()
                           .AddSingleton<ISecretProvider, SecretProvider>();
        }

        private static IServiceCollection AddInfrastructureProviders(this IServiceCollection services)
        {
            return services//IMessageContractProvider
                           .AddScoped<IMessageContractProvider, OneTimePasswordCreatedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, OneTimePasswordDeletedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, OneTimePasswordUsedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, OneTimePasswordResendedMessageContractProvider>()
                           .AddScoped<IMessageContractsProvider, MessageContractsProvider>()

                           .AddSingleton<IChannelTypesProvider, ChannelTypesProvider>();
        }
    }
}
