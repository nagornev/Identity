using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public abstract class KeyDeletionBackgroundProcessor<TKeyDeletionServiceType, TKeyOptionsType> : IBackgroundProcessor
        where TKeyDeletionServiceType : IKeyDeletionService
        where TKeyOptionsType : KeyOptions
    {
        private readonly string _job;

        private readonly IServiceProvider _serviceProvider;

        private readonly TKeyOptionsType _keyOptions;

        public KeyDeletionBackgroundProcessor(string jobName,
                                              IServiceProvider serviceProvider,
                                              IOptions<TKeyOptionsType> keyOptions)
        {
            _job = jobName;
            _serviceProvider = serviceProvider;
            _keyOptions = keyOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellation)
        {
            RecurringJob.AddOrUpdate(_job,
                                     () => ExecuteAsync(cancellation),
                                     _keyOptions.RotationInterval);

            return Task.CompletedTask;
        }

        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public async Task ExecuteAsync(CancellationToken cancellation = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                TKeyDeletionServiceType keyDeletionService = scope.ServiceProvider.GetRequiredService<TKeyDeletionServiceType>();
                await keyDeletionService.DeleteAsync(cancellation);
            }
        }
    }
}
