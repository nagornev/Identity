using Hangfire;
using Hangfire.PostgreSql;
using Notification.Api.Backgrounds;
using Notification.Backgrounds.Abstractions.Processors;
using Notification.Backgrounds.Processors;

namespace Notification.Api.Extensions.Startup
{
    public static class BackgroundStartupExtensions
    {
        private const string _connectionsStringName = "HangfireDbContext";

        public static IServiceCollection AddBackgrounds(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHangfire(options =>
            {
                options.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                       .UseSimpleAssemblyNameTypeSerializer()
                       .UseRecommendedSerializerSettings()
                       .UsePostgreSqlStorage(cfg => cfg.UseNpgsqlConnection(configuration.GetConnectionString(_connectionsStringName)), new PostgreSqlStorageOptions
                       {
                           DistributedLockTimeout = TimeSpan.FromMinutes(1)
                       });
            })
                        .AddHangfireServer()
                        .AddBackgroundProcessors();
        }

        private static IServiceCollection AddBackgroundProcessors(this IServiceCollection services)
        {
            return services.AddSingleton<IBackgroundProcessor, DeleteExpiredPendingNotificationMessageBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, DeleteExpiredNotificationMessageBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, OutboxBackgroundProcessor>()

                           .AddHostedService<BackgroundsProcessorsStarter>();
        }
    }
}
