using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

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

        private readonly IAccessKeyStorage _accessKeysStorage;

        private readonly IRefreshKeyStorage _refreshKeysStorage;

        private readonly IUnitOfWork _unitOfWork;

        public SignInConfirmService(ISignInValidationService signInValidationService,
                                    ISessionQueryService sessionQueryService,
                                    IUserQueryService userQueryService,
                                    IUserScopesService userScopesService,
                                    IOtpTokenPayloadProvider otpTokenPayloadProvider,
                                    IAccessTokenProvider accessTokenProvider,
                                    IRefreshTokenProvider refreshTokenProvider,
                                    IAccessKeyStorage accessKeysStorage,
                                    IRefreshKeyStorage refreshKeysStorage,
                                    IUnitOfWork unitOfWork)
        {
            _signInValidationService = signInValidationService;
            _sessionQueryService = sessionQueryService;
            _userQueryService = userQueryService;
            _userScopesService = userScopesService;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenPair> ConfirmAsync(Guid otpId,
                                                  string otp,
                                                  CancellationToken cancellation = default)
        {
            OtpContent otpContent = await _signInValidationService.ValidateOtpAsync(otpId, otp, cancellation);
            SignInOtpTokenPayload signInOtpTokenPayload = _otpTokenPayloadProvider.Deserialize<SignInOtpTokenPayload>(otpContent.Payload);

            Session session = await _sessionQueryService.GetSessionByIdAsync(signInOtpTokenPayload.SessionId);

            _signInValidationService.ValidateSession(session);

            KeyPair accessPrimaryKey = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPair refreshPrimaryKey = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            session.Activate();
            session.ChangeKidIfNeed(refreshPrimaryKey.Kid);

            User user = await _userQueryService.GetUserByIdAsync(otpContent.UserId, cancellation);
            IReadOnlyCollection<Scope> scopes = await _userScopesService.GetUserScopesAsync(user, session.Audience);

            await _unitOfWork.SaveAsync(cancellation);

            return new TokenPair(_accessTokenProvider.Create(new AccessTokenCreationParameters(user, session, scopes), accessPrimaryKey),
                                 _refreshTokenProvider.Create(new RefreshTokenCreationParameters(user, session), refreshPrimaryKey));
        }
    }
}
