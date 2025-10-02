using MassTransit;
using Notification.Application.Abstractions.Services;
using Notification.Application.Services;
using Notification.Messaging.Services;

using Otp.Persistence.Services;

namespace Notification.Api.Extensions.Startup
{
    public static class ServicesStartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationServices()
                           .AddInfrastructureServices();
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IActivateNotificationMessageCreateService, ActivateNotificationMessageCreateService>()
                           .AddScoped<IChannelNotificationMessageCreateService, ChannelNotificationMessageCreateService>()
                           .AddScoped<IDeleteExpiredNotificationMessageBackgroundService, DeleteExpiredNotificationMessageBackgroundService>()
                           .AddScoped<IDeleteExpiredPendingNotificationMessageBackgroundService, DeleteExpiredPendingNotificationMessageBackgroundService>()
                           .AddScoped<INotificationMessageCreateService, NotificationMessageCreateService>()
                           .AddScoped<INotificationMessageQueryService, NotificationMessageQueryService>()
                           .AddScoped<IUserQueryService, UserQueryService>()
                           .AddScoped<IEmailAddressChangedEventService, EmailAddressChangedEventService>()
                           .AddScoped<IUserActivatedEventService, UserActivatedEventService>()
                           .AddScoped<INotificationMessageSendService, NotificationMessageSendService>()
                           .AddScoped<IOneTimePasswordNotificationMessageCreateService, OneTimePasswordNotificationMessageCreateService>()
                           .AddScoped<IPendingNotificationMessageCreateService, PendingNotificationMessageCreateService>()
                           .AddScoped<IPendingNotificationMessageQueryService, PendingNotificationMessageQueryService>()
                           .AddScoped<IPendingNotificationMessageSendService, PendingNotificationMessageSendService>()
                           .AddSingleton<ILogService, LogService>();
        }

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IPublishEventService, PublishEventService>()
                           .AddScoped<IOutboxService, OutboxService>();
        }
    }
}
