using Auth.Api.Contracts;
using Auth.Api.Extensions;
using Auth.Application.DTOs;
using Auth.Application.Features.ChangeEmailAddress.Commands;
using Auth.Application.Features.ChangePassword.Commands;
using Auth.Application.Features.ChangePersonName;
using Auth.Application.Features.Refresh;
using Auth.Application.Features.SignIn.Queries;
using Auth.Application.Features.SignUp.Commands;
using Carter;
using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.FileSystemGlobbing;
using OperationResults;

namespace Auth.Api.Endpoints
{
    public class FeatureEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity");

            #region SignUp

            group.MapPost("sign/up", SignUp);
            group.MapGet("sign/up/activate/{token}", Activate);

            #endregion

            #region SignIn

            group.MapPost("sign/in", SignIn);
            group.MapPost("sign/in/confirm", Confirm);
            group.MapPost("token/refresh", RefreshTokenPair);

            #endregion

            #region Profile

            group.MapPost("change/email/request", ChangeEmailRequest)
                 .RequireAuthorization("edit:profile");
            group.MapPost("change/email/confirm", ChangeEmailConfirm)
                 .RequireAuthorization("edit:profile");
            group.MapGet("change/email/update/{token}", ChangeEmailUpdate);

            group.MapPost("change/password/request", ChangePasswordRequest)
                 .RequireAuthorization("edit:profile");
            group.MapPost("change/password/confirm", ChangePasswordConfirm)
                 .RequireAuthorization("edit:profile");

            group.MapPost("change/name", ChangePersonName)
                 .RequireAuthorization("edit:profile");
            #endregion

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
            Result<Otp> signInResult = await mediator.Send(query, cancellation);
            
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

        private static async Task<IResult> RefreshTokenPair(RefreshCommand command, IMediator mediator, CancellationToken cancellation = default)
        {
            Result<TokenPair> refreshResult = await mediator.Send(command, cancellation);

            return refreshResult.IsSuccess ?
                    Results.Ok(refreshResult) :
                    Results.BadRequest(refreshResult);
        }

        private static async Task<IResult> ChangeEmailRequest(RequestEmailAddressChangeContract contract, IMediator mediator, HttpContext context, CancellationToken cancellation = default)
        {
            Result<Otp> changeEmailRequestResult = await mediator.Send(new RequestEmailAddressChangeCommand(context.User.GetUserId(), contract.NewEmailAddress), cancellation);

            return changeEmailRequestResult.IsSuccess ?
                    Results.Ok(changeEmailRequestResult):
                    Results.BadRequest(changeEmailRequestResult);
        }

        private static async Task<IResult> ChangeEmailConfirm(ConfirmEmailAddressChangeCommand command, IMediator mediator, CancellationToken cancellation = default)
        {
            Result changeEmailConfirmResult = await mediator.Send(command, cancellation);

            return changeEmailConfirmResult.IsSuccess ?
                    Results.Ok(changeEmailConfirmResult):
                    Results.BadRequest(changeEmailConfirmResult);
        }

        private static async Task<IResult> ChangeEmailUpdate(string token, IMediator mediator, CancellationToken cancellation = default)
        {
            Result changeEmailUpdateResult = await mediator.Send(new EmailAddressUpdateCommand(token), cancellation);

            return changeEmailUpdateResult.IsSuccess ?
                    Results.Ok(changeEmailUpdateResult) :
                    Results.BadRequest(changeEmailUpdateResult);
        }

        private static async Task<IResult> ChangePasswordRequest(RequestPasswordChangeContract contract, IMediator mediator, HttpContext context, CancellationToken cancellation = default)
        {
            Result<Otp> changePasswordRequestResult = await mediator.Send(new RequestPasswordChangeCommand(context.User.GetUserId(), contract.OldPassword, contract.NewPassword), cancellation);

            return changePasswordRequestResult.IsSuccess ?
                    Results.Ok(changePasswordRequestResult):
                    Results.BadRequest(changePasswordRequestResult);
        }

        private static async Task<IResult> ChangePasswordConfirm(ConfirmPasswordChangeCommand command, IMediator mediator, CancellationToken cancellation =default)
        {
            Result changePasswordConfirmResult = await mediator.Send(command, cancellation);

            return changePasswordConfirmResult.IsSuccess?
                    Results.Ok(changePasswordConfirmResult):
                    Results.BadRequest(changePasswordConfirmResult);
        }

        private static async Task<IResult> ChangePersonName(PersonNameChangeContract contract, IMediator mediator, HttpContext context, CancellationToken cancellation = default) 
        {
            Result changePersonNameResult = await mediator.Send(new PersonNameChangeCommand(context.User.GetUserId(), contract.NewPersonName), cancellation);

            return changePersonNameResult.IsSuccess ?
                    Results.Ok(changePersonNameResult):
                    Results.BadRequest(changePersonNameResult);
        }
    }
}
