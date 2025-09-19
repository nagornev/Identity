using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Auth.Api.Services
{
    public class JwtDashboardAuthorizationFilter : IDashboardAsyncAuthorizationFilter
    {
        public async Task<bool> AuthorizeAsync([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var result = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            return result.Succeeded;
        }
    }
}