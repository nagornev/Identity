using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otp.Domain.Aggregates;

namespace Otp.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(u => u.EmailAddress, uea =>
            {
                uea.Property(ea => ea.Value)
                   .HasColumnName(nameof(User.EmailAddress))
                   .IsRequired();
            });

            builder.HasMany<OneTimePassword>()
                   .WithOne()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
