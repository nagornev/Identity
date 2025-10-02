using Auth.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Configurations
{
    internal class ScopeConfiguration : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.HasKey(s => s.Id);

            builder.OwnsOne(s => s.Audience, sa =>
            {
                sa.Property(a => a.Value)
                  .HasColumnName(nameof(Scope.Audience))
                  .IsRequired();
            });

            builder.OwnsOne(s => s.Action, sa =>
            {
                sa.Property(a => a.Value)
                  .HasColumnName(nameof(Scope.Action))
                  .IsRequired();
            });

            builder.OwnsOne(a => a.Resource, ar =>
            {
                ar.Property(r => r.Value)
                  .HasColumnName(nameof(Scope.Resource))
                  .IsRequired();
            });
        }
    }
}
