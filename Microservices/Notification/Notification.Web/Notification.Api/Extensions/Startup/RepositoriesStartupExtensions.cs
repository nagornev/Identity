using DDD.Repositories;
using Microsoft.EntityFrameworkCore;
using Notification.Domain.Aggregates;
using Notification.Persistence.Abstractions.Repositories;
using Notification.Persistence.Contexts;
using Notification.Persistence.Decorators;
using Notification.Persistence.Repositories;

namespace Notification.Api.Extensions.Startup
{
    public static class RepositoriesStartupExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddContext(configuration)
                           .AddUserRepository()
                           .AddNotificationMessageRepository()
                           .AddPendingNotificationMessageRepository()
                           .AddOutboxRepository()
                           .AddScoped<IUnitOfWork, UnitOfWork>();

        }

        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext))));
        }

        private static IServiceCollection AddUserRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<User>, UserReporitory>()
                           .Decorate<IRepository<User>, RepositoryExceptionDecorator<User>>()
                           .AddScoped<IRepositoryReader<User>>(s => s.GetRequiredService<IRepository<User>>())
                           .AddScoped<IRepositoryWriter<User>>(s => s.GetRequiredService<IRepository<User>>());
        }

        private static IServiceCollection AddNotificationMessageRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<NotificationMessage>, NotificationMessageRepository>()
                           .Decorate<IRepository<NotificationMessage>, RepositoryExceptionDecorator<NotificationMessage>>()
                           .AddScoped<IRepositoryReader<NotificationMessage>>(s => s.GetRequiredService<IRepository<NotificationMessage>>())
                           .AddScoped<IRepositoryWriter<NotificationMessage>>(s => s.GetRequiredService<IRepository<NotificationMessage>>());
        }

        private static IServiceCollection AddPendingNotificationMessageRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<PendingNotificationMessage>, PendingNotificationMessageRepository>()
                           .Decorate<IRepository<PendingNotificationMessage>, RepositoryExceptionDecorator<PendingNotificationMessage>>()
                           .AddScoped<IRepositoryReader<PendingNotificationMessage>>(s => s.GetRequiredService<IRepository<PendingNotificationMessage>>())
                           .AddScoped<IRepositoryWriter<PendingNotificationMessage>>(s => s.GetRequiredService<IRepository<PendingNotificationMessage>>());
        }

        private static IServiceCollection AddOutboxRepository(this IServiceCollection services)
        {
            return services.AddScoped<IOutboxRepository, OutboxRepository>()
                           .Decorate<IOutboxRepository, OutboxRepositoryExceptionDecorator>();
        }

    }
}
