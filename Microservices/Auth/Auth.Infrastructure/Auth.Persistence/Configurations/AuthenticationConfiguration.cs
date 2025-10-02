using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.PendingPasswordHash, apph =>
            {
                apph.OwnsOne(pph => pph.PasswordHash, ph =>
                {
                    ph.Property(p => p.Value)
                      .HasColumnName(nameof(Authentication.PendingPasswordHash));
                });

                apph.Property(pph => pph.Version)
                    .HasColumnName(nameof(Authentication.PendingPasswordHash) + nameof(PendingPasswordHash.Version));
            });

            builder.OwnsOne(a => a.PasswordHash, aph =>
            {
                aph.Property(ph => ph.Value)
                   .HasColumnName(nameof(Authentication.PasswordHash));
            });
        }
    }
}
