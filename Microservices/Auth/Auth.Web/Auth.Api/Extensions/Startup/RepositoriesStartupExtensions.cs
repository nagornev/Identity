using Auth.Domain.Aggregates;
using Auth.Persistence.Abstractions.Repositories;
using Auth.Persistence.Contexts;
using Auth.Persistence.Decorators;
using Auth.Persistence.Repositories;
using DDD.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Extensions.Startup
{
    public static class RepositoriesStartupExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddContext(configuration)
                           .AddUserRepoistory()
                           .AddSessionRepository()
                           .AddRoleRepository()
                           .AddScopeRepository()
                           .AddOutboxRepository()
                           .AddScoped<IUnitOfWork, UnitOfWork>();

        }

        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext))));
        }

        private static IServiceCollection AddUserRepoistory(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<User>, UserRepository>()
                           .Decorate<IRepository<User>, RepositoryExceptionDecorator<User>>()
                           .AddScoped<IRepositoryReader<User>>(s => s.GetRequiredService<IRepository<User>>())
                           .AddScoped<IRepositoryWriter<User>>(s => s.GetRequiredService<IRepository<User>>());
        }

        private static IServiceCollection AddSessionRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<Session>, SessionRepository>()
                           .Decorate<IRepository<Session>, RepositoryExceptionDecorator<Session>>()
                           .AddScoped<IRepositoryReader<Session>>(s => s.GetRequiredService<IRepository<Session>>())
                           .AddScoped<IRepositoryWriter<Session>>(s => s.GetRequiredService<IRepository<Session>>());
        }

        private static IServiceCollection AddRoleRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<Role>, RoleRepository>()
                           .Decorate<IRepository<Role>, RepositoryExceptionDecorator<Role>>()
                           .AddScoped<IRepositoryReader<Role>>(s => s.GetRequiredService<IRepository<Role>>())
                           .AddScoped<IRepositoryWriter<Role>>(s => s.GetRequiredService<IRepository<Role>>());
        }

        private static IServiceCollection AddScopeRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<Scope>, ScopeRepository>()
                           .Decorate<IRepository<Scope>, RepositoryExceptionDecorator<Scope>>()
                           .AddScoped<IRepositoryReader<Scope>>(s => s.GetRequiredService<IRepository<Scope>>())
                           .AddScoped<IRepositoryWriter<Scope>>(s => s.GetRequiredService<IRepository<Scope>>());
        }

        private static IServiceCollection AddOutboxRepository(this IServiceCollection services)
        {
            return services.AddScoped<IOutboxRepository, OutboxRepository>()
                           .Decorate<IOutboxRepository, OutboxRepositoryExceptionDecorator>();
        }
    }
}
