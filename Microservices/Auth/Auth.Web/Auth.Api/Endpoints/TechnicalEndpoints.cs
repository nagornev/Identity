using Carter;

namespace Auth.Api.Endpoints
{
    public class TechnicalEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity");

            group.MapGet("health", Health);
        }

        private static IResult Health(CancellationToken cancellation = default)
        {
            return Results.Ok();
        }
    }
}
