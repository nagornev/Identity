using Auth.Application.DTOs;
using Auth.Application.Features.SignIn.Queries;
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
            group.MapGet("sign/up/activate/{token}", Activate);
            group.MapPost("sign/in", SignIn);
            group.MapPost("sign/in/confirm", Confirm);


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

        private static async Task<IResult> SignIn(RequestUserSignInQuery query, IMediator mediator, CancellationToken cancellation = default)
        {
            Result<Guid> signInResult = await mediator.Send(query, cancellation);
            
            return signInResult.IsSuccess?
                    Results.Ok(signInResult) : 
                    Results.BadRequest(signInResult);
        }

        private static async Task<IResult> Confirm(ConfirmUserSignInQuery query, IMediator mediator, CancellationToken cancellation = default)
        {
            Result<TokenPair> confirmSignInResult = await mediator.Send(query, cancellation);

            return confirmSignInResult.IsSuccess ?
                    Results.Ok(confirmSignInResult) :
                    Results.BadRequest(confirmSignInResult);
        }

        private static async Task<IResult> Test(IRequestClient<OneTimePasswordCreationRequest> otpCreationRequestClient, CancellationToken cancellation = default)
        {

            //var response = await otpCreationRequestClient.GetResponse<OneTimePasswordCreationCompleted, Fault<OneTimePasswordCreationRequest>>(new OneTimePasswordCreationRequest(Guid.NewGuid,
            //                                                                                                             tag,
            //                                                                                                             payload),
            //                                                                                                     cancellation);

            //return response switch
            //{
            //    { Message: OneTimePasswordCreationCompleted completed } => completed.OneTimePasswordId,
            //    { Message: Fault<OneTimePasswordCreationRequest> fault } => throw new MessagingInvalidOperationInfrastructureException(fault.Message.ToString()!),

            //    _ => throw new MessagingInvalidOperationInfrastructureException("Unexpected response from OTP service.")
            //};

            return Results.Ok();
        }
    }
}
