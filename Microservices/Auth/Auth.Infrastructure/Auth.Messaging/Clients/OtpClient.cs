using Auth.Application.Abstractions.Clients;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Infrastructures.Messaging;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Clients
{
    public class OtpClient : IOtpClient
    {
        private readonly IRequestClient<OneTimePasswordCreationRequest> _otpCreationRequestClient;

        private readonly IRequestClient<OneTimePasswordValidationRequest> _otpValidationRequestClient;

        public OtpClient(IRequestClient<OneTimePasswordCreationRequest> otpCreationRequestClient,
                         IRequestClient<OneTimePasswordValidationRequest> otpValidationRequestClient)
        {
            _otpCreationRequestClient = otpCreationRequestClient;
            _otpValidationRequestClient = otpValidationRequestClient;
        }

        public async Task<Guid> CreateAsync(Guid subject, string tag, string payload = "", CancellationToken cancellation = default)
        {
            var response = await _otpCreationRequestClient.GetResponse<OneTimePasswordCreationCompleted>(new OneTimePasswordCreationRequest(subject,
                                                                                                                         tag,
                                                                                                                         payload),
                                                                                                                 cancellation);

            return response switch
            {
                { Message: OneTimePasswordCreationCompleted completed } => completed.OneTimePasswordId,

                _ => throw new MessagingInvalidOperationInfrastructureException("Unexpected response from OTP service.")
            };
        }

        public async Task<OtpValidation> ValidateAsync(Guid otpId, string otp, string tag, CancellationToken cancellation = default)
        {
            var response = await _otpValidationRequestClient.GetResponse<OneTimePasswordValidationCompleted, Fault<OneTimePasswordValidationRequest>>(new OneTimePasswordValidationRequest(
                                                                                                                                                otpId,
                                                                                                                                                otp,
                                                                                                                                                tag),
                                                                                                                       cancellation);

            return response switch
            {
                { Message: OneTimePasswordValidationCompleted completed } => new OtpValidation(completed.IsValid,
                                                                                               completed.Subject,
                                                                                               completed.Payload),
                { Message: Fault<OneTimePasswordValidationRequest> fault } => throw new MessagingInvalidOperationInfrastructureException(GetExceptionMessage(fault)),

                _ => throw new MessagingInvalidOperationInfrastructureException("Unexpected response from OTP service.")
            };
        }

        private string GetExceptionMessage<T>(Fault<T> faultMessage)
        {
            return faultMessage.Exceptions.FirstOrDefault()?.Message ?? "Unknown error.";
        }
    }
}
