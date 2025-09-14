using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class SignInValidationService : ISignInValidationService
    {
        private readonly IOtpValidationService _otpValidationService;

        private readonly IWindowValidationService _windowValidationService;

        private readonly ISessionValidationService _sessionValidationService;

        private readonly IFingerprintValidationService _fingerprintValidationService;

        private readonly IFingerprintMessageProvider _fingerprintMessageProvider;

        public SignInValidationService(IOtpValidationService otpValidationService,
                                       IWindowValidationService windowValidationService,
                                       ISessionValidationService sessionValidationService,
                                       IFingerprintValidationService fingerprintValidationService,
                                       IFingerprintMessageProvider fingerprintMessageProvider)
        {
            _otpValidationService = otpValidationService;
            _windowValidationService = windowValidationService;
            _sessionValidationService = sessionValidationService;
            _fingerprintValidationService = fingerprintValidationService;
            _fingerprintMessageProvider = fingerprintMessageProvider;
        }

        public void ValidateWindow(long timestamp, int window)
        {
            _windowValidationService.Validate(timestamp, window);
        }

        public void ValidateSession(Session session)
        {
            _sessionValidationService.ValidateWithoutActive(session);
        }

        public void ValidateFingerprint(Guid otpId, string otp, string newPublicKey, long timestamp, string signature, Session session)
        {
            _fingerprintValidationService.Validate(new FingerprintValidationParameters(_fingerprintMessageProvider.GetMessage(otpId, otp, newPublicKey, timestamp),
                                                                                       signature),
                                                   session.PublicKey);
        }

        public async Task<OtpContent> ValidateOtpAsync(Guid otpId, string otp, CancellationToken cancellation = default)
        {
            return await _otpValidationService.ValidateAsync(otpId, otp, OtpTags.SignIn, cancellation);
        }
    }
}
