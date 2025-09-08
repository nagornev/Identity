using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Providers;
using Auth.Messaging.Abstractions.Providers;
using Auth.Messaging.Providers;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Providers;

namespace Auth.Api.Extensions.Startup
{
    public static class ProvidersStartupExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationProviders()
                           .AddInfrastructureProviders();
        }

        private static IServiceCollection AddApplicationProviders(this IServiceCollection services)
        {
            return services.AddSingleton<ITimeProvider, Application.Providers.TimeProvider>()
                           .AddSingleton<IFingerprintMessageProvider, FingerprintMessageProvider>();
        }

        private static IServiceCollection AddInfrastructureProviders(this IServiceCollection services)
        {
            return services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>()
                           .AddSingleton<IRefreshTokenProvider, RefreshTokenProvider>()
                           .AddSingleton<IChannelTokenProvider, ChannelTokenProvider>()
                           .AddSingleton<IRefreshTokenPayloadProvider, RefreshTokenPayloadProvider>()
                           .AddSingleton<IChannelTokenPayloadProvider, ChannelTokenPayloadProvider>()
                           .AddSingleton<IOtpTokenPayloadProvider, OtpTokenPayloadProvider>()
                           .AddSingleton<ITokenKidProvider, TokenKidProvider>()
                           .AddSingleton<IPasswordHashProvider, PasswordHashProvider>()

                           //Infrastructure only
                           .AddSingleton<IAccessClientProvider, AccessVaultClientProvider>()
                           .AddSingleton<IRefreshClientProvider, RefreshVaultClientProvider>()
                           .AddSingleton<IChannelClientProvider, ChannelVaultClientProvider>()
                           .AddSingleton<IJwtClaimsProvider, JwtClaimsProvider>()

                           //ISecurityKeyProvider
                           .AddSingleton<ISecurityKeyProvider, RsaSecurityKeyProvider>()
                           .AddSingleton<ISecurityKeysProvider, SecurityKeysProvider>()

                           //IMessageContractProvider
                           .AddScoped<IMessageContractProvider, EmailAddressChangeConfirmedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, EmailAddressChangedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, PasswordHashChangedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, RolePermissionDeletedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, ScopePermissionDeletedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, SessionClosedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, UserActivatedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, UserCreatedMessageContractProvider>()
                           .AddScoped<IMessageContractProvider, UserDeletedMessageContractProvider>()
                           .AddScoped<IMessageContractsProvider, MessageContractsProvider>();
        }
    }
}
