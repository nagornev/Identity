using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Backgrounds.Processors
{
    public class DeleteUnactivatedUsersBackgroundProcessors : IDeleteUnactivatedUsersBackgroundProcessor
    {
        private const string _job = "delete-unactivated-users";

        private readonly IServiceProvider _serviceProvider;

        public DeleteUnactivatedUsersBackgroundProcessors(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellation)
        {
            RecurringJob.AddOrUpdate(_job,
                                     () => ExecuteAsync(cancellation),
                                     Cron.Daily);

            return Task.CompletedTask;
        }

        [AutomaticRetry(Attempts = 10)]
        public async Task ExecuteAsync(CancellationToken cancellation = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IDeleteUnactivatedUsersBackgroundService deleteUnactivatedUsersBackgroundService = scope.ServiceProvider.GetRequiredService<IDeleteUnactivatedUsersBackgroundService>();
                await deleteUnactivatedUsersBackgroundService.DeleteUnactivatedUsersAsync(cancellation);
            }
        }
    }
}
