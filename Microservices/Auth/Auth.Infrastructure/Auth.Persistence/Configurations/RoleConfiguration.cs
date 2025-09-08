using Auth.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.HasMany(e => e.Entitlements)
                   .WithOne()
                   .HasForeignKey(e => e.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
