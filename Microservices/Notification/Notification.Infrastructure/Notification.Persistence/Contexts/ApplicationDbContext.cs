using Microsoft.EntityFrameworkCore;
using Notification.Domain.Aggregates;
using Notification.Persistence.Configurations;
using Notification.Persistence.Entities;

namespace Notification.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<NotificationMessage> NotificationMessages { get; private set; }

        public DbSet<PendingNotificationMessage> PendingNotificationMessages { get; private set; }

        public DbSet<OutboxMessage> Outbox { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new NotificationMessageConfiguration())
                        .ApplyConfiguration(new PendingNotificationMessageConfiguration());

        }
    }
}
