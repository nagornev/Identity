using Microsoft.EntityFrameworkCore;
using Otp.Domain.Aggregates;
using Otp.Persistence.Configurations;
using Otp.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<OneTimePassword> OneTimePasswords { get; private set; }

        public DbSet<OutboxMessage> Outbox { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OneTimePasswordConfiguration());
        }
    }
}
