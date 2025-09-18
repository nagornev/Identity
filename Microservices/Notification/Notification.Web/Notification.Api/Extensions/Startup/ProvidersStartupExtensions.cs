using Notification.Application.Abstractions.Providers;
using Notification.Application.Providers;
using Notification.Messaging.Abstractions.Providers;
using Notification.Messaging.Providers;
using TimeProvider = Notification.Application.Providers.TimeProvider;

namespace Notification.Api.Extensions.Startup
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
                           .AddSingleton<IMessageSenderProvider, MessageSenderProvider>()

                           .AddSingleton<IMessageTextProvider, ActivateMessageTextProvider>()
                           .AddSingleton<IMessageTextProvider, ChannelMessageTextProvider>()
                           .AddSingleton<IMessageTextProvider, OtpMessageTextProvider>()
                           .AddSingleton<IMessageTextsProvider, MessageTextsProvider>();
        }

        private static IServiceCollection AddInfrastructureProviders(this IServiceCollection services)
        {
            return services//IMessageContractProvider
                           .AddScoped<IMessageContractProvider, NotificationMessageCreatedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, PendingNotificationMessageCreatedMessageContractProvider>()
                           .AddScoped<IMessageContractsProvider, MessageContractsProvider>()

                           .AddSingleton<IChannelTypeProvider, ChannelTypeProvider>();
        }
    }
}
