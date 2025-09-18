using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Services;
using Notification.Application.Options;
using Notification.Backgrounds.Abstractions.Processors;

namespace Notification.Backgrounds.Processors
{
    public class DeleteExpiredNotificationMessageBackgroundProcessor : IDeleteExpiredNotificationMessageBackgroundProcessor
    {
        private const string _job = "delete-expired-notification-message";

        private readonly IServiceProvider _serviceProvider;

        private readonly NotificationMessageOptions _notificationMessageOptions;

        public DeleteExpiredNotificationMessageBackgroundProcessor(IServiceProvider serviceProvider,
                                                                   IOptions<NotificationMessageOptions> notificationMessageOptions)
        {
            _serviceProvider = serviceProvider;
            _notificationMessageOptions = notificationMessageOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellation = default)
        {
            RecurringJob.AddOrUpdate(_job,
                                     () => ExecuteAsync(cancellation),
                                     _notificationMessageOptions.DeletionInterval);

            return Task.CompletedTask;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task ExecuteAsync(CancellationToken cancellation = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IDeleteExpiredNotificationMessageBackgroundService deleteExpiredNotificationMessageBackgroundService = scope.ServiceProvider.GetRequiredService<IDeleteExpiredNotificationMessageBackgroundService>();
                await deleteExpiredNotificationMessageBackgroundService.DeleteAsync(cancellation);
            }
        }
    }
}
