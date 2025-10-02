using MassTransit;
using Otp.Application.Abstractions.Clients;
using Otp.Messaging.Clients;
using Otp.Messaging.Consumers;
using Otp.Messaging.Options;

namespace Otp.Api.Extensions.Startup
{
    public static class ClientsStartupExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMassTransitClient(configuration)
                           .AddScoped<INotificationClient, NotificationClient>();
        }

        private static IServiceCollection AddMassTransitClient(this IServiceCollection services, IConfiguration configuration)
        {
            MessageBrokerOptions messageBrokerOptions = configuration.GetSection(nameof(MessageBrokerOptions))
                                                                     .Get<MessageBrokerOptions>()!;

            return services.AddMassTransit(options =>
            {
                options.SetKebabCaseEndpointNameFormatter();

                options.AddConsumer<OneTimePasswordCreatedConsumer>();
                options.AddConsumer<OneTimePasswordUsedConsumer>();
                options.AddConsumer<OneTimePasswordCreationRequestConsumer>();
                options.AddConsumer<OneTimePasswordValidationRequestConsumer>();
                options.AddConsumer<EmailAddressChangedConsumer>();
                options.AddConsumer<UserActivatedConsumer>();
                options.AddConsumer<OneTimePasswordResendedConsumer>();
                options.AddConsumer<LogConsumer>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerOptions.Host, messageBrokerOptions.Port, "/", h =>
                    {
                        h.Username(messageBrokerOptions.Username);
                        h.Password(messageBrokerOptions.Password);
                    });

                    cfg.ReceiveEndpoint("otp-one-time-password-created-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-one-time-password-used-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordUsedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-one-time-password-creation-request-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordCreationRequestConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-one-time-password-validation-request-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordValidationRequestConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-email-address-changed-queue", e =>
                    {
                        e.ConfigureConsumer<EmailAddressChangedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-user-activated-queue", e =>
                    {
                        e.ConfigureConsumer<UserActivatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-one-time-password-resended-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordResendedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("otp-log-queue", e =>
                    {
                        e.ConfigureConsumer<LogConsumer>(context);
                    });
                });


            });
        }
    }
}
