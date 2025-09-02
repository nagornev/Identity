using Auth.Application.Abstractions.Clients;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Infrastructures.Messaging;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Clients
{
    public class OtpClient : IOtpClient
    {
        private readonly IRequestClient<OtpCreationRequest> _otpCreationClient;

        private readonly IRequestClient<OtpValidationRequest> _otpValidationClient;

        public OtpClient(IRequestClient<OtpCreationRequest> otpCreationClient,
                         IRequestClient<OtpValidationRequest> otpValidationClient)
        {
            _otpCreationClient = otpCreationClient;
            _otpValidationClient = otpValidationClient;
        }

        public async Task<string> CreateAsync(Guid subject, string tag, string payload = "", CancellationToken cancellation = default)
        {
            var response = await _otpCreationClient.GetResponse<OtpCreationCompleted, Fault<OtpCreationRequest>>(new OtpCreationRequest(subject,
                                                                                                                                        tag,
                                                                                                                                        payload),
                                                                                                                 cancellation);

            return response switch
            {
                { Message: OtpCreationCompleted completed } => completed.Token,
                { Message: Fault<OtpCreationRequest> fault } => throw new MessagingInvalidOperationInfrastructureException(GetExceptionMessage(fault)),

                _ => throw new MessagingInvalidOperationInfrastructureException("Unexpected response from OTP service.")
            };
        }

        public async Task<OtpValidation> ValidateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default)
        {
            var response = await _otpValidationClient.GetResponse<OtpValidationCompleted, Fault<OtpValidationRequest>>(new OtpValidationRequest(otpToken,
                                                                                                                                                otp,
                                                                                                                                                tag),
                                                                                                                       cancellation);

            return response switch
            {
                { Message: OtpValidationCompleted completed } => new OtpValidation(completed.IsValid,
                                                                                   completed.Subject,
                                                                                   completed.Payload),
                { Message: Fault<OtpValidationRequest> fault } => throw new MessagingInvalidOperationInfrastructureException(GetExceptionMessage(fault)),

                _ => throw new MessagingInvalidOperationInfrastructureException("Unexpected response from OTP service.")
            };
        }

        private string GetExceptionMessage<T>(Fault<T> faultMessage)
        {
            return faultMessage.Exceptions.FirstOrDefault()?.Message ?? "Unknown error.";
        }
    }
}
