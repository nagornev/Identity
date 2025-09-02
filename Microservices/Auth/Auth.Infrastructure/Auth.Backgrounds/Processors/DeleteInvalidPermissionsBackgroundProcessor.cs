using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Backgrounds.Processors
{
    public class DeleteInvalidPermissionsBackgroundProcessor : IDeleteInvalidPermissionsBackgroundProcessor
    {
        private const string _job = "delete-invalid-permissions";

        private readonly IServiceProvider _serviceProvider;

        public DeleteInvalidPermissionsBackgroundProcessor(IServiceProvider serviceProvider)
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
                IDeleteInvalidPermissionsBackgroundService deleteInvalidPermissionsBackgroundService = scope.ServiceProvider.GetRequiredService<IDeleteInvalidPermissionsBackgroundService>();
                await deleteInvalidPermissionsBackgroundService.DeleteInvalidPermissionsAsync(cancellation);
            }
        }
    }
}
