using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;


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
        }
    }
}
