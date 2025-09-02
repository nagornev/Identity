using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Auth.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, ApplicationOptions options)
        {
            await context.Database.MigrateAsync();

            if (!context.Roles.Any(x => x.Name == options.BasicRoleName))
            {
                Role basicRole = Role.Create(options.BasicRoleName);
                await context.Roles.AddAsync(basicRole);
                await context.SaveChangesAsync();
            }
        }
    }
}
