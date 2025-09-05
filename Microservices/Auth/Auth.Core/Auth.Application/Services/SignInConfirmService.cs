using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using DDD.Repositories;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class SignInConfirmService : ISignInConfirmService
    {
        private readonly ISignInValidationService _signInValidationService;

        private readonly ISessionQueryService _sessionQueryService;

        private readonly IUserQueryService _userQueryService;

        private readonly IUserScopesService _userScopesService;

        private readonly IOtpTokenPayloadProvider _otpTokenPayloadProvider;

        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly IAccessKeyStorage _accessKeysStorage;

        private readonly IRefreshKeyStorage _refreshKeysStorage;

        private readonly WindowOptions _options;

        private readonly IUnitOfWork _unitOfWork;

        public SignInConfirmService(ISignInValidationService signInValidationService,
                                    ISessionQueryService sessionQueryService,
                                    IUserQueryService userQueryService,
                                    IUserScopesService userScopesService,
                                    IOtpTokenPayloadProvider otpTokenPayloadProvider,
                                    IAccessTokenProvider accessTokenProvider,
                                    IRefreshTokenProvider refreshTokenProvider,
                                    ITimeProvider timeProvider,
                                    IAccessKeyStorage accessKeysStorage,
                                    IRefreshKeyStorage refreshKeysStorage,
                                    IOptions<WindowOptions> options,
                                    IUnitOfWork unitOfWork)
        {
            _signInValidationService = signInValidationService;
            _sessionQueryService = sessionQueryService;
            _userQueryService = userQueryService;
            _userScopesService = userScopesService;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _timeProvider = timeProvider;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _options = options.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenPair> ConfirmAsync(Guid otpId,
                                                  string otp,
                                                  string newPublicKey,
                                                  long timestamp,
                                                  string signature,
                                                  CancellationToken cancellation = default)
        {
            _signInValidationService.ValidateWindow(timestamp, _options.Window);

            OtpContent otpContent = await _signInValidationService.ValidateOtpAsync(otpId, otp, cancellation);
            SignInOtpTokenPayload signInOtpTokenPayload = _otpTokenPayloadProvider.Deserialize<SignInOtpTokenPayload>(otpContent.Payload);

            Session session = await _sessionQueryService.GetSessionByIdAsync(signInOtpTokenPayload.SessionId);

            _signInValidationService.ValidateSession(session);
            _signInValidationService.ValidateFingerprint(otpId, otp, timestamp, signature, session);

            KeyPair accessPrimaryKey = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPair refreshPrimaryKey = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            session.Activate();
            session.ChangeKidIfNeed(refreshPrimaryKey.Kid);
            session.Update(newPublicKey, _timeProvider.NowUnix());

            User user = await _userQueryService.GetUserByIdAsync(otpContent.Subject, cancellation);
            IReadOnlyCollection<Scope> scopes = await _userScopesService.GetUserScopesAsync(user, session.Audience);

            await _unitOfWork.SaveAsync(cancellation);

            return new TokenPair(_accessTokenProvider.Create(new AccessTokenCreationParameters(user, session, scopes), accessPrimaryKey),
                                 _refreshTokenProvider.Create(new RefreshTokenCreationParameters(user, session), refreshPrimaryKey));
        }
    }
}
