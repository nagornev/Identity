using Auth.Api.Abstractions.Providers;
using Auth.Api.Providers;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Providers;
using Auth.Messaging.Abstractions.Providers;
using Auth.Messaging.Providers;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Providers;
using OperationResults;

namespace Auth.Api.Extensions.Startup
{
    public static class ProvidersStartupExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationProviders()
                           .AddInfrastructureProviders()
                           .AddWebProviders();
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

        private static IServiceCollection AddWebProviders(this IServiceCollection services)
        {
            return services.AddSingleton<IResultProvider>(new ResultProviderBuilder()
                                                           .UseSuccess((options) => options
                                                               .UseNocontentFactory(result => Results.Ok(result)))
                                                           .UseFailed((options) => options
                                                               .UseFactory(result => result.Error.Type switch
                                                               {
                                                                   ResultErrorTypes.Null => Results.BadRequest(result),
                                                                   ResultErrorTypes.Empty => Results.BadRequest(result),
                                                                   ResultErrorTypes.NotFound => Results.NotFound(result),
                                                                   ResultErrorTypes.Canceled => Results.NoContent(),
                                                                   ResultErrorTypes.Invalid => Results.BadRequest(result),
                                                                   ResultErrorTypes.InvalidFomat => Results.BadRequest(result),
                                                                   ResultErrorTypes.Already => Results.BadRequest(result),
                                                                   ResultErrorTypes.NotSupported => Results.BadRequest(result),
                                                                   ResultErrorTypes.OutOfRange => Results.BadRequest(result),
                                                                   ResultErrorTypes.Unconfirmed => Results.BadRequest(result),
                                                                   ResultErrorTypes.Unavailable => Results.StatusCode(500),

                                                                   _ => Results.BadRequest(result),
                                                               }))
                                                           .Build());
        }
    }
}
