using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Aggregates;
using Notification.Domain.ValueObjects;

namespace Notification.Persistence.Configurations
{
    public class PendingNotificationMessageConfiguration : IEntityTypeConfiguration<PendingNotificationMessage>
    {
        public void Configure(EntityTypeBuilder<PendingNotificationMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(nm => nm.Channel, nmc =>
            {
                nmc.Property(c => c.Type)
                   .HasColumnName(nameof(NotificationMessage.Channel) + nameof(Channel.Type));

                nmc.Property(c => c.Value)
                   .HasColumnName(nameof(NotificationMessage.Channel) + nameof(Channel.Value));
            });
        }
    }
}
