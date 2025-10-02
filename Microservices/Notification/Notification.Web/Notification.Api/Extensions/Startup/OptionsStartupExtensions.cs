using Microsoft.Extensions.Options;
using Notification.Application.Options;
using Notification.Messaging.Options;

namespace Notification.Api.Extensions.Startup
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
            return services.AddSingleton(Options.Create(configuration.GetSection(nameof(NotificationMessageOptions))
                                                                     .Get<NotificationMessageOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(PendingNotificationMessageOptions))
                                                                     .Get<PendingNotificationMessageOptions>()!))
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
