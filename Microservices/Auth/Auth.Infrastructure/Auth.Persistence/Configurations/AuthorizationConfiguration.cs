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

            builder.Ignore(a => a.RolePermissions);
            builder.Ignore(a => a.ScopePermissions);

            builder.HasMany<RolePermission>("_rolePermissions")
                   .WithOne()
                   .HasForeignKey(rp => rp.AuthorizationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ScopePermission>("_scopePermissions")
                   .WithOne()
                   .HasForeignKey(sp => sp.AuthorizationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
