using Carter;
using Hangfire;

namespace Notification.Api.Endpoints
{
    public class TechnicalEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/notification");

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
