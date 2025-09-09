using Carter;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Api.Endpoints
{
    public class TestEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("test", async (IOneTimeCreateService createService) =>
            {
                Guid oneTimePassword = await createService.CreateAsync("test", Guid.NewGuid());

                return Results.Ok(oneTimePassword);
            });
        }
    }
}
