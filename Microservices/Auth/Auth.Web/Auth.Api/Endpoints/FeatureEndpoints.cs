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
using OperationResults;

namespace Auth.Api.Endpoints
{
    public class FeatureEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity");

            #region SignUp

            group.MapPost("sign/up", SignUp)
                 .WithValidation<RequestUserSignUpContract>();
            group.MapGet("sign/up/activate/{token}", Activate);

            #endregion

            #region SignIn

            group.MapPost("sign/in", SignIn)
                 .WithValidation<RequestUserSignInContract>();
            group.MapPost("sign/in/confirm", Confirm)
                 .WithValidation<OtpContract>();
            group.MapPost("token/refresh", RefreshTokenPair)
                 .WithValidation<RefreshContract>();

            #endregion

            #region Profile

            group.MapPost("change/email/request", ChangeEmailRequest)
                 .WithValidation<RequestEmailAddressChangeContract>()
                 .RequireAuthorization("edit:profile");
            group.MapPost("change/email/confirm", ChangeEmailConfirm)
                 .WithValidation<OtpContract>()
                 .RequireAuthorization("edit:profile");
            group.MapGet("change/email/update/{token}", ChangeEmailUpdate);

            group.MapPost("change/password/request", ChangePasswordRequest)
                 .WithValidation<RequestPasswordChangeContract>()
                 .RequireAuthorization("edit:profile");
            group.MapPost("change/password/confirm", ChangePasswordConfirm)
                 .WithValidation<OtpContract>()
                 .RequireAuthorization("edit:profile");

            group.MapPost("change/name", ChangePersonName)
                 .WithValidation<PersonNameChangeContract>()
                 .RequireAuthorization("edit:profile");
            #endregion

        }

        private static async Task<IResult> SignUp(RequestUserSignUpContract contract,
                                                  IMediator mediator,
                                                  CancellationToken cancellation = default)
        {
            Result signUpResult = await mediator.Send(new RequestUserSignUpCommand(contract.EmailAddress,
                                                                                   contract.PersonName,
                                                                                   contract.Password),
                                                      cancellation);

            return signUpResult.IsSuccess ?
                    Results.Ok(signUpResult) :
                    Results.BadRequest(signUpResult);
        }

        private static async Task<IResult> Activate(string token,
                                                    IMediator mediator,
                                                    CancellationToken cancellation = default)
        {
            Result activateResult = await mediator.Send(new ConfirmUserSignUpCommand(token), cancellation);

            return activateResult.IsSuccess ?
                    Results.Ok(activateResult) :
                    Results.BadRequest(activateResult);
        }

        private static async Task<IResult> SignIn(RequestUserSignInContract contract,
                                                  HttpContext httpContext,
                                                  IMediator mediator,
                                                  CancellationToken cancellation = default)
        {
            Result<Otp> signInResult = await mediator.Send(new RequestUserSignInQuery(contract.EmailAddress,
                                                                                      contract.Password,
                                                                                      contract.Audience,
                                                                                      contract.PublicKey,
                                                                                      httpContext.GetRequestContext()),
                                                           cancellation);

            return signInResult.IsSuccess ?
                    Results.Ok(signInResult) :
                    Results.BadRequest(signInResult);
        }

        private static async Task<IResult> Confirm(OtpContract contract,
                                                   IMediator mediator,
                                                   CancellationToken cancellation = default)
        {
            Result<TokenPair> confirmSignInResult = await mediator.Send(new ConfirmUserSignInQuery(contract.OtpId,
                                                                                                   contract.Otp),
                                                                        cancellation);

            return confirmSignInResult.IsSuccess ?
                    Results.Ok(confirmSignInResult) :
                    Results.BadRequest(confirmSignInResult);
        }

        private static async Task<IResult> RefreshTokenPair(RefreshContract contract,
                                                            HttpContext httpContext,
                                                            IMediator mediator,
                                                            CancellationToken cancellation = default)
        {
            Result<TokenPair> refreshResult = await mediator.Send(new RefreshCommand(contract.RefreshToken,
                                                                                     contract.NewPublicKey,
                                                                                     contract.Timestamp,
                                                                                     contract.Signature,
                                                                                     httpContext.GetRequestContext()),
                                                                  cancellation);

            return refreshResult.IsSuccess ?
                    Results.Ok(refreshResult) :
                    Results.BadRequest(refreshResult);
        }

        private static async Task<IResult> ChangeEmailRequest(RequestEmailAddressChangeContract contract,
                                                              HttpContext context,
                                                              IMediator mediator,
                                                              CancellationToken cancellation = default)
        {
            Result<Otp> changeEmailRequestResult = await mediator.Send(new RequestEmailAddressChangeCommand(context.User.GetUserId(),
                                                                                                            contract.NewEmailAddress),
                                                                       cancellation);

            return changeEmailRequestResult.IsSuccess ?
                    Results.Ok(changeEmailRequestResult) :
                    Results.BadRequest(changeEmailRequestResult);
        }

        private static async Task<IResult> ChangeEmailConfirm(OtpContract contract,
                                                              IMediator mediator,
                                                              CancellationToken cancellation = default)
        {
            Result changeEmailConfirmResult = await mediator.Send(new ConfirmEmailAddressChangeCommand(contract.OtpId,
                                                                                                       contract.Otp),
                                                                  cancellation);

            return changeEmailConfirmResult.IsSuccess ?
                    Results.Ok(changeEmailConfirmResult) :
                    Results.BadRequest(changeEmailConfirmResult);
        }

        private static async Task<IResult> ChangeEmailUpdate(string token,
                                                             IMediator mediator,
                                                             CancellationToken cancellation = default)
        {
            Result changeEmailUpdateResult = await mediator.Send(new EmailAddressUpdateCommand(token), cancellation);

            return changeEmailUpdateResult.IsSuccess ?
                    Results.Ok(changeEmailUpdateResult) :
                    Results.BadRequest(changeEmailUpdateResult);
        }

        private static async Task<IResult> ChangePasswordRequest(RequestPasswordChangeContract contract,
                                                                 HttpContext context,
                                                                 IMediator mediator,
                                                                 CancellationToken cancellation = default)
        {
            Result<Otp> changePasswordRequestResult = await mediator.Send(new RequestPasswordChangeCommand(context.User.GetUserId(),
                                                                                                           contract.OldPassword,
                                                                                                           contract.NewPassword),
                                                                          cancellation);

            return changePasswordRequestResult.IsSuccess ?
                    Results.Ok(changePasswordRequestResult) :
                    Results.BadRequest(changePasswordRequestResult);
        }

        private static async Task<IResult> ChangePasswordConfirm(OtpContract contract,
                                                                 IMediator mediator,
                                                                 CancellationToken cancellation = default)
        {
            Result changePasswordConfirmResult = await mediator.Send(new ConfirmPasswordChangeCommand(contract.OtpId,
                                                                                                      contract.Otp),
                                                                     cancellation);

            return changePasswordConfirmResult.IsSuccess ?
                    Results.Ok(changePasswordConfirmResult) :
                    Results.BadRequest(changePasswordConfirmResult);
        }

        private static async Task<IResult> ChangePersonName(PersonNameChangeContract contract,
                                                            HttpContext context,
                                                            IMediator mediator,
                                                            CancellationToken cancellation = default)
        {
            Result changePersonNameResult = await mediator.Send(new PersonNameChangeCommand(context.User.GetUserId(),
                                                                                            contract.NewPersonName),
                                                                cancellation);

            return changePersonNameResult.IsSuccess ?
                    Results.Ok(changePersonNameResult) :
                    Results.BadRequest(changePersonNameResult);
        }
    }
}
