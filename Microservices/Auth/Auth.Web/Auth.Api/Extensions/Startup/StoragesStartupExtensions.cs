using Auth.Application.Abstractions.Storages;
using Auth.Security.Storages;

namespace Auth.Api.Extensions.Startup
{
    public static class StoragesStartupExtensions
    {
        public static IServiceCollection AddStorages(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton<IAccessKeyStorage, AccessKeyStorage>()
                           .AddSingleton<IRefreshKeyStorage, RefreshKeyStorage>()
                           .AddSingleton<IChannelKeyStorage, ChannelKeyStorage>();
        }
    }
}
