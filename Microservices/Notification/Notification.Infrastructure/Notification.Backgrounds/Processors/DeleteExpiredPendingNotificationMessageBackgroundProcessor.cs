using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Services;
using Notification.Application.Options;
using Notification.Backgrounds.Abstractions.Processors;

namespace Notification.Backgrounds.Processors
{
    public class DeleteExpiredPendingNotificationMessageBackgroundProcessor : IDeleteExpiredPendingNotificationMessageBackgroundProcessor
    {
        private const string _job = "delete-expired-pending-notification-message";

        private readonly IServiceProvider _serviceProvider;

        private readonly PendingNotificationMessageOptions _pendingNotificationMessageOptions;

        public DeleteExpiredPendingNotificationMessageBackgroundProcessor(IServiceProvider serviceProvider,
                                                                          IOptions<PendingNotificationMessageOptions> pendingNotificationMessageOptions)
        {
            _serviceProvider = serviceProvider;
            _pendingNotificationMessageOptions = pendingNotificationMessageOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellation = default)
        {
            RecurringJob.AddOrUpdate(_job,
                                     () => ExecuteAsync(cancellation),
                                     _pendingNotificationMessageOptions.DeletionInterval);

            return Task.CompletedTask;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task ExecuteAsync(CancellationToken cancellation = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IDeleteExpiredPendingNotificationMessageBackgroundService deleteExpiredPendingNotificationMessageBackgroundService = scope.ServiceProvider.GetRequiredService<IDeleteExpiredPendingNotificationMessageBackgroundService>();
                await deleteExpiredPendingNotificationMessageBackgroundService.DeleteAsync(cancellation);
            }
        }
    }
}
