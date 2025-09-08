using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class AuthorizationConfiguration : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Navigation(a => a.RolePermissions).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(a => a.ScopePermissions).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(a => a.RolePermissions)
                   .WithOne()
                   .HasForeignKey(rp => rp.AuthorizationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.ScopePermissions)
                   .WithOne()
                   .HasForeignKey(sp => sp.AuthorizationId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
