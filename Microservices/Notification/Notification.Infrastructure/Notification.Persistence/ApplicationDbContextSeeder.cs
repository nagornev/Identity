using Microsoft.EntityFrameworkCore;
using Notification.Persistence.Contexts;

namespace Notification.Persistence
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
        }
    }
}
