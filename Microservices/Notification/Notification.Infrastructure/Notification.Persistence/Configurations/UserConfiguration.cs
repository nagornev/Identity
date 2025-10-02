using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Aggregates;

namespace Notification.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(u => u.EmailAddress, uea =>
            {
                uea.Property(ea => ea.Value)
                   .HasColumnName(nameof(User.EmailAddress))
                   .IsRequired();
            });

            builder.HasMany<NotificationMessage>()
                   .WithOne()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
