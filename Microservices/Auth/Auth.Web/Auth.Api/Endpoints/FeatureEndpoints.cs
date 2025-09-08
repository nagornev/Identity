using Auth.Application.Features.SignUp.Commands;
using Carter;
using MassTransit;
using MediatR;
using MessageContracts;
using OperationResults;

namespace Auth.Api.Endpoints
{
    public class FeatureEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity");

            group.MapPost("sign/up", SignUp);
            group.MapGet("sign/activate/{token}", Activate);


            group.MapGet("test", Test);
        }

        private static async Task<IResult> SignUp(RequestUserSignUpCommand command, IMediator mediator, CancellationToken cancellation = default)
        {
            Result signUpResult = await mediator.Send(command, cancellation);

            return signUpResult.IsSuccess ?
                    Results.Ok(signUpResult) :
                    Results.BadRequest(signUpResult);
        }

        private static async Task<IResult> Activate(string token, IMediator mediator, CancellationToken cancellation = default)
        {
            Result activateResult = await mediator.Send(new ConfirmUserSignUpCommand(token), cancellation);

            return activateResult.IsSuccess ?
                    Results.Ok(activateResult) :
                    Results.BadRequest(activateResult);
        }

        private static async Task<IResult> Test(IPublishEndpoint publisher, CancellationToken cancellation = default)
        {
            IMessageContract messageContract = new UserCreatedMessageContract(Guid.NewGuid(), "test@ya.ru");

            await publisher.Publish(messageContract, messageContract.GetType(), cancellation);

            return Results.Ok();
        }
    }
}
