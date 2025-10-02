using Microsoft.EntityFrameworkCore;
using Otp.Persistence.Contexts;

namespace Otp.Persistence
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
        }
    }
}
