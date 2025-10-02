using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Factories;
using Auth.Security.Factories;

namespace Auth.Api.Extensions.Startup
{
    public static class FactoriesStartupExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationFactories()
                           .AddInfastructureFactories();

        }

        public static IServiceCollection AddApplicationFactories(this IServiceCollection services)
        {
            return services.AddSingleton<IUserFactory, UserFactory>()
                           .AddSingleton<ISessionFactory, SessionFactory>()
                           .AddSingleton<ISaltFactory, SaltFactory>();
        }

        public static IServiceCollection AddInfastructureFactories(this IServiceCollection services)
        {
            return services.AddSingleton<IAccessKeyPairFactory, AccessKeyFactory>()
                           .AddSingleton<IRefreshKeyPairFactory, RefreshKeyFactory>()
                           .AddSingleton<IChannelKeyPairFactory, ChannelKeyFactory>();
        }
    }
}
