using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otp.Domain.Aggregates;
using Otp.Domain.ValueObjects;


namespace Otp.Persistence.Configurations
{
    internal class OneTimePasswordConfiguration : IEntityTypeConfiguration<OneTimePassword>
    {
        public void Configure(EntityTypeBuilder<OneTimePassword> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(otp => otp.Secret, ps =>
            {
                ps.Property(s => s.Value)
                  .HasColumnName(nameof(OneTimePassword.Secret))
                  .IsRequired();
            });

            builder.OwnsOne(otp => otp.Channel, pc =>
            {
                pc.Property(c => c.Type)
                  .HasColumnName(nameof(Channel) + nameof(Channel.Type))
                  .IsRequired();

                pc.Property(c => c.Value)
                  .HasColumnName(nameof(Channel) + nameof(Channel.Value))
                  .IsRequired();
            });
        }
    }
}
