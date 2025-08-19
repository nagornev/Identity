using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.PasswordHash, aph =>
            {
                aph.Property(ph => ph.Value)
                   .HasColumnName(nameof(Authentication.PasswordHash));
            });
        }
    }
}
