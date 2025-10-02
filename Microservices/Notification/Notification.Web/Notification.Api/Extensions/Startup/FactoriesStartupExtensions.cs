using Notification.Application.Abstractions.Factories;
using Notification.Application.Factories;

namespace Notification.Api.Extensions.Startup
{
    public static class FactoriesStartupExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationFactories();

        }

        private static IServiceCollection AddApplicationFactories(this IServiceCollection services)
        {
            return services.AddSingleton<INotificationMessageFactory, NotificationMessageFactory>()
                           .AddSingleton<IPendingNotificationMessageFactory, PendingNotificationMessageFactory>()
                           .AddSingleton<IUserFactory, UserFactory>();
        }
    }
}
