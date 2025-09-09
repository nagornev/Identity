using Auth.Application.Options;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Api.Extensions.Startup
{
    public static class OptionsStartupExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationOptions(configuration)
                           .AddInfrastructureOptions(configuration);
        }

        private static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton(Options.Create(configuration.GetSection(nameof(ApplicationOptions))
                                                                     .Get<ApplicationOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(WindowOptions))
                                                                     .Get<WindowOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(AccessTokenOptions))
                                                                     .Get<AccessTokenOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(RefreshTokenOptions))
                                                                     .Get<RefreshTokenOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(ChannelTokenOptions))
                                                                     .Get<ChannelTokenOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(AccessKeyOptions))
                                                                     .Get<AccessKeyOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(RefreshKeyOptions))
                                                                     .Get<RefreshKeyOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(ChannelKeyOptions))
                                                                     .Get<ChannelKeyOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(SaltOptions))
                                                                     .Get<SaltOptions>()!));
        }

        private static IServiceCollection AddInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton(Options.Create(configuration.GetSection(nameof(AccessStorageClientOptions))
                                                                     .Get<AccessStorageClientOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(RefreshStorageClientOptions))
                                                                     .Get<RefreshStorageClientOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(ChannelStorageClientOptions))
                                                                     .Get<ChannelStorageClientOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(AccessKeyStorageOptions))
                                                                     .Get<AccessKeyStorageOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(RefreshKeyStorageOptions))
                                                                     .Get<RefreshKeyStorageOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(ChannelKeyStorageOptions))
                                                                     .Get<ChannelKeyStorageOptions>()!))
                           .AddSingleton(Options.Create(configuration.GetSection(nameof(PasswordHashOptions))
                                                                     .Get<PasswordHashOptions>()!))
                           ;
        }
    }
}
