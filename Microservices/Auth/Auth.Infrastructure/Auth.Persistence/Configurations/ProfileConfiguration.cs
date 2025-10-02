using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.PendingEmailAddress, ppea =>
            {
                ppea.OwnsOne(pea => pea.EmailAddress, ea =>
                {
                    ea.Property(ea => ea.Value)
                      .HasColumnName(nameof(Profile.PendingEmailAddress));
                });

                ppea.Property(pea => pea.Version)
                    .HasColumnName(nameof(Profile.PendingEmailAddress) + nameof(PendingEmailAddress.Version));

                ppea.Property(pea => pea.IsConfirmed)
                    .HasColumnName(nameof(Profile.PendingEmailAddress) + nameof(PendingEmailAddress.IsConfirmed));
            });

            builder.OwnsOne(p => p.EmailAddress, pea =>
            {
                pea.Property(ea => ea.Value)
                   .HasColumnName(nameof(Profile.EmailAddress));
            });

            builder.OwnsOne(p => p.PersonName, pun =>
            {
                pun.Property(un => un.Name)
                   .HasColumnName(nameof(Profile.PersonName));
            });
        }
    }
}
