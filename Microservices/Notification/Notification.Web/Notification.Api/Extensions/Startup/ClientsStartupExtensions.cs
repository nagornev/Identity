using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Senders;
using Notification.Application.Senders;
using Notification.Messaging.Consumers;
using Notification.Messaging.Options;

namespace Notification.Api.Extensions.Startup
{
    public static class ClientsStartupExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMassTransitClient(configuration)
                           .AddSenders();
        }

        private static IServiceCollection AddMassTransitClient(this IServiceCollection services, IConfiguration configuration)
        {
            MessageBrokerOptions messageBrokerOptions = configuration.GetSection(nameof(MessageBrokerOptions))
                                                                     .Get<MessageBrokerOptions>()!;

            return services.AddMassTransit(options =>
            {
                options.SetKebabCaseEndpointNameFormatter();


                options.AddConsumer<ActivateNotificationConsumer>();
                options.AddConsumer<ChannelNotificationConsumer>();
                options.AddConsumer<OneTimePasswordNotificationConsumer>();
                options.AddConsumer<NotificationMessageCreatedConsumer>();
                options.AddConsumer<PendingNotificationMessageCreatedConsumer>();
                options.AddConsumer<EmailAddressChangedConsumer>();
                options.AddConsumer<UserActivatedConsumer>();
                options.AddConsumer<LogConsumer>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerOptions.Host, messageBrokerOptions.Port, "/", h =>
                    {
                        h.Username(messageBrokerOptions.Username);
                        h.Password(messageBrokerOptions.Password);
                    });

                    cfg.ReceiveEndpoint("notification-activate-notification-queue", e =>
                    {
                        e.ConfigureConsumer<ActivateNotificationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-channel-notification-queue", e =>
                    {
                        e.ConfigureConsumer<ChannelNotificationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-one-time-password-notification-queue", e =>
                    {
                        e.ConfigureConsumer<OneTimePasswordNotificationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-notification-message-created-queue", e =>
                    {
                        e.ConfigureConsumer<NotificationMessageCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-pending-notification-message-created-queue", e =>
                    {
                        e.ConfigureConsumer<PendingNotificationMessageCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-user-activated-queue", e =>
                    {
                        e.ConfigureConsumer<UserActivatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-email-address-changed-queue", e =>
                    {
                        e.ConfigureConsumer<EmailAddressChangedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("notification-log-queue", e =>
                    {
                        e.ConfigureConsumer<LogConsumer>(context);
                    });
                });
            });
        }

        private static IServiceCollection AddSenders(this IServiceCollection services)
        {
            return services.AddSingleton<IMessageSender, EmailMessageSender>()
                           .AddSingleton<IMessageSender, SmsMessageSender>();
        }
    }
}
