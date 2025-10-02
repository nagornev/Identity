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

            await AddRoleAndScopesAsync(context, options.Roles.Basic, options.Issuer);
            await AddRoleAndScopesAsync(context, options.Roles.Owner, options.Issuer);

            await context.SaveChangesAsync();
        }

        private static async Task AddRoleAndScopesAsync(ApplicationDbContext context, ApplicationOptions.Role role, string issuer)
        {
            Role? dbRole = await context.Roles.Include(x => x.Entitlements)
                                              .FirstOrDefaultAsync(x => x.Name == role.Name);

            if (dbRole == null)
            {
                dbRole = Role.Create(role.Name);
                await context.Roles.AddAsync(dbRole);

                foreach (ApplicationOptions.Scope scope in role.Scopes)
                {
                    Scope? dbScope = await context.Scopes.FirstOrDefaultAsync(x => x.Audience.Value == issuer &&
                                                                                    x.Action.Value == scope.Action &&
                                                                                    x.Resource.Value == scope.Resource);


                    if (dbScope == null)
                    {
                        dbScope = Scope.Create(issuer, scope.Action, scope.Resource, scope.Description);
                        await context.Scopes.AddAsync(dbScope);
                        dbRole.AssignEntitlement(dbScope.Id);
                    }
                    else
                    {
                        dbRole.AssignEntitlement(dbScope.Id);
                    }
                }
            }
            else
            {
                foreach (ApplicationOptions.Scope scope in role.Scopes)
                {
                    Scope? dbScope = await context.Scopes.FirstOrDefaultAsync(x => x.Audience.Value == issuer &&
                                                                                    x.Action.Value == scope.Action &&
                                                                                    x.Resource.Value == scope.Resource);

                    if (dbScope == null)
                    {
                        dbScope = Scope.Create(issuer, scope.Action, scope.Resource, scope.Description);
                        await context.Scopes.AddAsync(dbScope);
                        dbRole.AssignEntitlement(dbScope.Id);
                    }
                    else if (!dbRole.HasEntitlement(x => x.ScopeId == dbScope.Id))
                    {
                        dbRole.AssignEntitlement(dbScope.Id);
                    }
                }
            }
        }
    }
}
