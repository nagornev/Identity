using Otp.Application.Abstractions.Services;
using Otp.Application.Services;
using Otp.Messaging.Services;
using Otp.Persistence.Services;

namespace Otp.Api.Extensions.Startup
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
            return services.AddScoped<IDeleteInvalidOneTimePasswordsBackgroundService, DeleteInvalidOneTimePasswordsBackgroundService>()
                           .AddScoped<IOneTimeCreateService, OneTimeCreateService>()
                           .AddScoped<IOneTimePasswordCreatedEventService, OneTimePasswordCreatedEventService>()
                           .AddScoped<IOneTimePasswordQueryService, OneTimePasswordQueryService>()
                           .AddScoped<IOneTimePasswordUsedEventService, OneTimePasswordUsedEventService>()
                           .AddScoped<IOneTimePasswordValidationService, OneTimePasswordValidationService>()
                           .AddScoped<IUserQueryService, UserQueryService>()
                           .AddScoped<IEmailAddressChangedEventService, EmailAddressChangedEventService>()
                           .AddScoped<IUserActivatedEventService, UserActivatedEventService>()
                           .AddScoped<IOneTimePasswordResendService, OneTimePasswordResendService>()
                           .AddSingleton<ILogService, LogService>();
        }

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IPublishEventService, PublishEventService>()
                           .AddScoped<IOutboxService, OutboxService>();
        }
    }
}
