using Auth.Application.Abstractions.Storages;
using Auth.Security.Storages;

namespace Auth.Api.Extensions.Startup
{
    public static class StoragesStartupExtensions
    {
        public static IServiceCollection AddStorages(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddScoped<IAccessKeyStorage, AccessKeyStorage>()
                           .AddScoped<IRefreshKeyStorage, RefreshKeyStorage>()
                           .AddScoped<IChannelKeyStorage, ChannelKeyStorage>();
        }
    }
}
