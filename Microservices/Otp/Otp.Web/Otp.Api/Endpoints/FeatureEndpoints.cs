using Carter;
using MediatR;
using OperationResults;
using Otp.Api.Contracts;
using Otp.Api.Extensions;
using Otp.Application.Features.Resend;

namespace Otp.Api.Endpoints
{
    public class FeatureEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/otp");

            group.MapPost("resend", Resend)
                 .WithValidation<ResendContract>();
        }


        private static async Task<IResult> Resend(ResendContract contract, IMediator mediator, CancellationToken cancellation)
        {
            Result resendResult = await mediator.Send(new ResendCommand(contract.OneTimePasswordId), cancellation);

            return resendResult.IsSuccess ?
                    Results.Ok(resendResult) :
                    Results.BadRequest(resendResult);
        }
    }
}
