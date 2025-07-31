using Auth.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Extensions
{
    internal static class DbSetExtensions
    {
        public static IQueryable<User> WithIncludes(this DbSet<User> dbSet)
        {
            return dbSet.Include(x => x.Authentication)
                        .Include(x => x.Authorization)
                            .ThenInclude(x => "_rolePermissions")
                        .Include(x => x.Authorization)
                            .ThenInclude(x => "_scopePermissions")
                        .Include(x => x.Profile);
        }

        public static IQueryable<Role> WithIncludes(this DbSet<Role> dbSet)
        {
            return dbSet.Include(x => "_entitlements");
        }
    }
}
