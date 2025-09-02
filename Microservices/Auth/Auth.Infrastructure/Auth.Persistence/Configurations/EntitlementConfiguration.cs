using Auth.Domain.Aggregates;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class EntitlementConfiguration : IEntityTypeConfiguration<Entitlement>
    {
        public void Configure(EntityTypeBuilder<Entitlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.RoleId);

            //builder.HasOne<Role>()
            //       .WithMany()
            //       .HasForeignKey(e => e.RoleId)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Scope>()
                   .WithMany()
                   .HasForeignKey(e => e.ScopeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
