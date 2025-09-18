using Auth.Api.Backgrounds;
using Auth.Backgrounds.Abstractions.Processors;
using Auth.Backgrounds.Processors;
using Hangfire;
using Hangfire.PostgreSql;

namespace Auth.Api.Extensions.Startup
{
    public static class BackgroundsStartupExtensions
    {
        private const string _connectionsStringName = "HangfireDbContext";

        public static IServiceCollection AddBackgrounds(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHangfire(options =>
                            {
                                options.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                       .UseSimpleAssemblyNameTypeSerializer()
                                       .UseRecommendedSerializerSettings()
                                       .UsePostgreSqlStorage(cfg => cfg.UseNpgsqlConnection(configuration.GetConnectionString(_connectionsStringName)));
                            })
                           .AddHangfireServer()
                           .AddBackgroundProcessors();
        }

        private static IServiceCollection AddBackgroundProcessors(this IServiceCollection services)
        {
            return services
                           .AddSingleton<IBackgroundProcessor, AccessKeysRotationBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, RefreshKeyRotationBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, ChannelKeyRotationBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, AccessKeyDeletionBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, RefreshKeyDeletionBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, ChannelKeyDeletionBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, DeleteUnactivatedUsersBackgroundProcessors>()
                           .AddSingleton<IBackgroundProcessor, DeleteInvalidSessionsBackgroundProcessors>()
                           .AddSingleton<IBackgroundProcessor, DeleteInvalidPermissionsBackgroundProcessor>()
                           .AddSingleton<IBackgroundProcessor, OutboxBackgroundProcessor>()

                           .AddHostedService<BackgroundsProcessorsStarter>();
        }
    }
}
