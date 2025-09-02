using Auth.Domain.Aggregates;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => rp.Id);

            builder.HasIndex(rp => rp.AuthorizationId);

            //builder.HasOne<Authorization>()
            //       .WithMany()
            //       .HasForeignKey(rp => rp.AuthorizationId)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Role>()
                   .WithMany()
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
