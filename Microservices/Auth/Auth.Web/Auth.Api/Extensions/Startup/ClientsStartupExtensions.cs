using Auth.Application.Abstractions.Clients;
using Auth.Messaging.Clients;
using Auth.Messaging.Consumers;
using Auth.Messaging.Options;
using MassTransit;
using MessageContracts;

namespace Auth.Api.Extensions.Startup
{
    public static class ClientsStartupExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMassTransitClient(configuration)
                           .AddScoped<IOtpClient, OtpClient>()
                           .AddScoped<INotificationClient, NotificationClient>();
        }

        private static IServiceCollection AddMassTransitClient(this IServiceCollection services, IConfiguration configuration)
        {
            MessageBrokerOptions messageBrokerOptions = configuration.GetSection(nameof(MessageBrokerOptions))
                                                                     .Get<MessageBrokerOptions>()!;

            return services.AddMassTransit(options =>
            {
                options.SetKebabCaseEndpointNameFormatter();

                options.AddConsumer<EmailAddressChangeConfirmedMessageConsumer>();
                options.AddConsumer<PasswordHashChangedMessageConsumer>();
                options.AddConsumer<UserCreatedMessageConsumer>();
                options.AddConsumer<LogConsumer>();

                options.AddRequestClient<OneTimePasswordCreationRequest>();
                options.AddRequestClient<OneTimePasswordValidationRequest>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerOptions.Host, messageBrokerOptions.Port, "/", h =>
                    {
                        h.Username(messageBrokerOptions.Username);
                        h.Password(messageBrokerOptions.Password);
                    });

                    cfg.ReceiveEndpoint("auth-email-address-change-confirmed-queue", e =>
                    {
                        e.ConfigureConsumer<EmailAddressChangeConfirmedMessageConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("auth-password-hash-changed-queue", e =>
                    {
                        e.ConfigureConsumer<PasswordHashChangedMessageConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("auth-user-created-queue", e =>
                    {
                        e.ConfigureConsumer<UserCreatedMessageConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("auth-log-queue", e =>
                    {
                        e.ConfigureConsumer<LogConsumer>(context);
                    });
                });
            });
        }
    }
}
