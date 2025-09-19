using Auth.Application.Abstractions.Services;
using Auth.Application.Services;
using Auth.Messaging.Services;
using Auth.Persistence.Services;

namespace Auth.Api.Extensions.Startup
{
    public static class ServicesStartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApplicationServices()
                           .AddInfrastructureServices();
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IAccessKeyRotationService, AccessKeyRotationService>()
                           .AddScoped<IChannelKeyRotationService, ChannelKeyRotationService>()
                           .AddScoped<IChannelTokenValidationService, ChannelTokenValidationService>()
                           .AddScoped<IDeleteInvalidPermissionsBackgroundService, DeleteInvalidPermissionsBackgroundService>()
                           .AddScoped<IDeleteInvalidSessionsBackgroundService, DeleteInvalidSessionsBackgroundService>()
                           .AddScoped<IDeleteUnactivatedUsersBackgroundService, DeleteUnactivatedUsersBackgroundService>()
                           .AddScoped<IEmailAddressChangeConfirmedEventService, EmailAddressChangeConfirmedEventService>()
                           .AddScoped<IEmailAddressChangeConfirmService, EmailAddressChangeConfirmService>()
                           .AddScoped<IEmailAddressChangeRequestService, EmailAddressChangeRequestService>()
                           .AddScoped<IEmailAddressUpdateService, EmailAddressUpdateService>()
                           .AddScoped<IFingerprintValidationService, FingerprintValidationService>()
                           .AddScoped<ILogoutService, LogoutService>()
                           .AddScoped<IOtpValidationService, OtpValidationService>()
                           .AddScoped<IPasswordChangeConfirmService, PasswordChangeConfirmService>()
                           .AddScoped<IPasswordChangeRequestService, PasswordChangeRequestService>()
                           .AddScoped<IPasswordHashChangedEventService, PasswordHashChangedEventService>()
                           .AddScoped<IPersonNameService, PersonNameService>()
                           .AddScoped<IRefreshKeyRotationService, RefreshKeyRotationService>()
                           .AddScoped<IRefreshService, RefreshService>()
                           .AddScoped<IRefreshTokenValidationService, RefreshTokenValidationService>()
                           .AddScoped<IRefreshValidationService, RefreshValidationService>()
                           .AddScoped<IRoleQueryService, RoleQueryService>()
                           .AddScoped<IScopeQueryService, ScopeQueryService>()
                           .AddScoped<ISessionQueryService, SessionQueryService>()
                           .AddScoped<ISessionValidationService, SessionValidationService>()
                           .AddScoped<ISignInConfirmService, SignInConfirmService>()
                           .AddScoped<ISignInRequestService, SignInRequestService>()
                           .AddScoped<ISignInValidationService, SignInValidationService>()
                           .AddScoped<ISignUpConfirmService, SignUpConfirmService>()
                           .AddScoped<ISignUpRequestService, SignUpRequestService>()
                           .AddScoped<IUserCreatedEventService, UserCreatedEventService>()
                           .AddScoped<IUserCreateService, UserCreateService>()
                           .AddScoped<IUserInitializeService, UserInitializeService>()
                           .AddScoped<IUserQueryService, UserQueryService>()
                           .AddScoped<IUserScopesService, UserScopesService>()
                           .AddScoped<IAccessKeyDeletionService, AccessKeyDeletionService>()
                           .AddScoped<IRefreshKeyDeletionService, RefreshKeyDeletionService>()
                           .AddScoped<IChannelKeyDeletionService, ChannelKeyDeletionService>()
                           .AddScoped<IWindowValidationService, WindowValidationService>()
                           .AddSingleton<ITokenKidService, TokenKidService>()
                           .AddSingleton<ILogService, LogService>();
        }

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IPublishEventService, PublishEventService>()
                           .AddScoped<IOutboxService, OutboxService>();
        }
    }
}
