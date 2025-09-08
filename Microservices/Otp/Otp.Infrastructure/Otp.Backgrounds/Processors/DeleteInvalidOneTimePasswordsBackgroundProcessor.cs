using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Otp.Application.Abstractions.Services;
using Otp.Application.Options;
using Otp.Backgrounds.Abstractions.Processors;

namespace Otp.Backgrounds.Processors
{
    public class DeleteInvalidOneTimePasswordsBackgroundProcessor : IDeleteInvalidOneTimePasswordsBackgroundProcessor
    {
        private const string _job = "delete-invalid-one-time-passwords";

        private readonly IServiceProvider _serviceProvider;

        private readonly OneTimePasswordOptions _oneTimePasswordOptions;

        public DeleteInvalidOneTimePasswordsBackgroundProcessor(IServiceProvider serviceProvider,
                                                                IOptions<OneTimePasswordOptions> oneTimePasswordOptions)
        {
            _serviceProvider = serviceProvider;
            _oneTimePasswordOptions = oneTimePasswordOptions.Value;
        }
        public Task StartAsync(CancellationToken cancellation)
        {
            RecurringJob.AddOrUpdate(_job,
                                     () => ExecuteAsync(cancellation),
                                     _oneTimePasswordOptions.DeletionInterval);

            return Task.CompletedTask;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task ExecuteAsync(CancellationToken cancellation = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IDeleteInvalidOneTimePasswordsBackgroundService deleteInvalidOneTimePasswordBackgroundService = scope.ServiceProvider.GetRequiredService<IDeleteInvalidOneTimePasswordsBackgroundService>();
                await deleteInvalidOneTimePasswordBackgroundService.DeleteAsync(cancellation);
            }
        }
    }
}
