using DDD.Repositories;
using Microsoft.EntityFrameworkCore;
using Otp.Domain.Aggregates;
using Otp.Persistence.Abstractions.Repositories;
using Otp.Persistence.Contexts;
using Otp.Persistence.Decorators;
using Otp.Persistence.Repositories;

namespace Otp.Api.Extensions.Startup
{
    public static class RepositoriesStartupExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddContext(configuration)
                           .AddUserRepository()
                           .AddOneTimePasswordRepository()
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

        private static IServiceCollection AddOneTimePasswordRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<OneTimePassword>, OneTimePasswordRepository>()
                           .Decorate<IRepository<OneTimePassword>, RepositoryExceptionDecorator<OneTimePassword>>()
                           .AddScoped<IRepositoryReader<OneTimePassword>>(s => s.GetRequiredService<IRepository<OneTimePassword>>())
                           .AddScoped<IRepositoryWriter<OneTimePassword>>(s => s.GetRequiredService<IRepository<OneTimePassword>>());
        }

        private static IServiceCollection AddOutboxRepository(this IServiceCollection services)
        {
            return services.AddScoped<IOutboxRepository, OutboxRepository>()
                           .Decorate<IOutboxRepository, OutboxRepositoryExceptionDecorator>();
        }

    }
}
