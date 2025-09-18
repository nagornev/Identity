using Auth.Api.Services;
using Carter;
using Hangfire;
using Hangfire.Dashboard;

namespace Auth.Api.Endpoints
{
    public class TechnicalEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity");

            group.MapGet("health", Health);
            group.MapHangfireDashboard()
                 .RequireAuthorization("read:scheduler");
        }

        private static IResult Health(CancellationToken cancellation = default)
        {
            return Results.Ok();
        }
    }
}
