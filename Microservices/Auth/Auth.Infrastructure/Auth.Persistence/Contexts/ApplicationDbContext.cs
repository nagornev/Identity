using Auth.Domain.Aggregates;
using Auth.Persistence.Configurations;
using Auth.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<Session> Sessions { get; private set; }

        public DbSet<Role> Roles { get; private set; }

        public DbSet<Scope> Scopes { get; private set; }

        internal DbSet<OutboxMessage> Outbox { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthenticationConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorizationConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new ScopePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new EntitlementConfiguration());
            modelBuilder.ApplyConfiguration(new ScopeConfiguration());
        }
    }
}
