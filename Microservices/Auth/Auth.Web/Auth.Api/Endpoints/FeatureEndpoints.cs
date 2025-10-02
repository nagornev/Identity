using Auth.Api.Abstractions.Providers;
using Auth.Api.Contracts;
using Auth.Api.Extensions;
using Auth.Application.DTOs;
using Auth.Application.Features.ChangeEmailAddress.Commands;
using Auth.Application.Features.ChangePassword.Commands;
using Auth.Application.Features.ChangePersonName;
using Auth.Application.Features.Logout;
using Auth.Application.Features.LogoutAll;
using Auth.Application.Features.Refresh;
using Auth.Application.Features.SignIn.Queries;
using Auth.Application.Features.SignUp.Commands;
using Carter;
using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using OperationResults;
using System.ComponentModel.Design;

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

            #region Loguot

            group.MapPost("logout", Logout)
                 .RequireAuthorization("edit:profile");

            group.MapPost("logout/all", LogoutAll)
                 .RequireAuthorization("edit:profile");

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
                                                  IResultProvider resultProvider,
                                                  CancellationToken cancellation = default)
        {
            Result signUpResult = await mediator.Send(new RequestUserSignUpCommand(contract.EmailAddress,
                                                                                   contract.PersonName,
                                                                                   contract.Password),
                                                      cancellation);

            return resultProvider.GetResult(signUpResult);
        }

        private static async Task<IResult> Activate(string token,
                                                    IMediator mediator,
                                                    IResultProvider resultProvider,
                                                    CancellationToken cancellation = default)
        {
            Result activateResult = await mediator.Send(new ConfirmUserSignUpCommand(token), cancellation);

            return resultProvider.GetResult(activateResult);
        }

        private static async Task<IResult> SignIn(RequestUserSignInContract contract,
                                                  HttpContext httpContext,
                                                  IMediator mediator,
                                                  IResultProvider resultProvider,
                                                  CancellationToken cancellation = default)
        {
            Result<Otp> signInResult = await mediator.Send(new RequestUserSignInQuery(contract.EmailAddress,
                                                                                      contract.Password,
                                                                                      contract.Audience,
                                                                                      contract.PublicKey,
                                                                                      httpContext.GetRequestContext()),
                                                           cancellation);

            return resultProvider.GetResult(signInResult);
        }

        private static async Task<IResult> Confirm(OtpContract contract,
                                                   IMediator mediator,
                                                   IResultProvider resultProvider,
                                                   CancellationToken cancellation = default)
        {
            Result<TokenPair> confirmSignInResult = await mediator.Send(new ConfirmUserSignInQuery(contract.OtpId,
                                                                                                   contract.Otp),
                                                                        cancellation);

            return resultProvider.GetResult(confirmSignInResult);
        }

        private static async Task<IResult> RefreshTokenPair(RefreshContract contract,
                                                            HttpContext httpContext,
                                                            IMediator mediator,
                                                            IResultProvider resultProvider,
                                                            CancellationToken cancellation = default)
        {
            Result<TokenPair> refreshResult = await mediator.Send(new RefreshCommand(contract.RefreshToken,
                                                                                     contract.NewPublicKey,
                                                                                     contract.Timestamp,
                                                                                     contract.Signature,
                                                                                     httpContext.GetRequestContext()),
                                                                  cancellation);

            return resultProvider.GetResult(refreshResult);
        }

        private static async Task<IResult> Logout(HttpContext httpContext,
                                                  IMediator mediator,
                                                  IResultProvider resultProvider,
                                                  CancellationToken cancellation  = default)
        {
            Result logoutResult = await mediator.Send(new LogoutCommand(httpContext.User.GetSessionId()), cancellation);

            return resultProvider.GetResult(logoutResult);
        }

        private static async Task<IResult> LogoutAll(HttpContext httpContext,
                                                     IMediator mediator,
                                                     IResultProvider resultProvider,
                                                     CancellationToken cancellation = default)
        {
            Result logoutAllResult = await mediator.Send(new LogoutAllCommand(httpContext.User.GetUserId()), cancellation);

            return resultProvider.GetResult(logoutAllResult);
        }

        private static async Task<IResult> ChangeEmailRequest(RequestEmailAddressChangeContract contract,
                                                              HttpContext context,
                                                              IMediator mediator,
                                                              IResultProvider resultProvider,
                                                              CancellationToken cancellation = default)
        {
            Result<Otp> changeEmailRequestResult = await mediator.Send(new RequestEmailAddressChangeCommand(context.User.GetUserId(),
                                                                                                            contract.NewEmailAddress),
                                                                       cancellation);

            return resultProvider.GetResult(changeEmailRequestResult);
        }

        private static async Task<IResult> ChangeEmailConfirm(OtpContract contract,
                                                              IMediator mediator,
                                                              IResultProvider resultProvider,
                                                              CancellationToken cancellation = default)
        {
            Result changeEmailConfirmResult = await mediator.Send(new ConfirmEmailAddressChangeCommand(contract.OtpId,
                                                                                                       contract.Otp),
                                                                  cancellation);

            return resultProvider.GetResult(changeEmailConfirmResult);
        }

        private static async Task<IResult> ChangeEmailUpdate(string token,
                                                             IMediator mediator,
                                                             IResultProvider resultProvider,
                                                             CancellationToken cancellation = default)
        {
            Result changeEmailUpdateResult = await mediator.Send(new EmailAddressUpdateCommand(token), cancellation);

            return resultProvider.GetResult(changeEmailUpdateResult);
        }

        private static async Task<IResult> ChangePasswordRequest(RequestPasswordChangeContract contract,
                                                                 HttpContext context,
                                                                 IMediator mediator,
                                                                 IResultProvider resultProvider,
                                                                 CancellationToken cancellation = default)
        {
            Result<Otp> changePasswordRequestResult = await mediator.Send(new RequestPasswordChangeCommand(context.User.GetUserId(),
                                                                                                           contract.OldPassword,
                                                                                                           contract.NewPassword),
                                                                          cancellation);

            return resultProvider.GetResult(changePasswordRequestResult);
        }

        private static async Task<IResult> ChangePasswordConfirm(OtpContract contract,
                                                                 IMediator mediator,
                                                                 IResultProvider resultProvider,
                                                                 CancellationToken cancellation = default)
        {
            Result changePasswordConfirmResult = await mediator.Send(new ConfirmPasswordChangeCommand(contract.OtpId,
                                                                                                      contract.Otp),
                                                                     cancellation);

            return resultProvider.GetResult(changePasswordConfirmResult);
        }

        private static async Task<IResult> ChangePersonName(PersonNameChangeContract contract,
                                                            HttpContext context,
                                                            IMediator mediator,
                                                            IResultProvider resultProvider,
                                                            CancellationToken cancellation = default)
        {
            Result changePersonNameResult = await mediator.Send(new PersonNameChangeCommand(context.User.GetUserId(),
                                                                                            contract.NewPersonName),
                                                                cancellation);

            return resultProvider.GetResult(changePersonNameResult);
        }
    }
}
