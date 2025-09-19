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
    public class RefreshService : IRefreshService
    {
        private readonly IRefreshValidationService _refreshValidationService;

        private readonly ISessionQueryService _sessionQueryService;

        private readonly IUserQueryService _userQueryService;

        private readonly IUserScopesService _userScopesService;

        private readonly ITokenKidService _tokenKidService;

        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly IAccessKeyStorage _accessKeyStorage;

        private readonly IRefreshKeyStorage _refreshKeyStorage;

        private readonly WindowOptions _options;

        private readonly IUnitOfWork _unitOfWork;

        public RefreshService(IRefreshValidationService refreshValidationService,
                              ISessionQueryService sessionQueryService,
                              IUserQueryService userQueryService,
                              IUserScopesService userScopesService,
                              ITokenKidService tokenKidService,
                              IAccessTokenProvider accessTokenProvider,
                              IRefreshTokenProvider refreshTokenProvider,
                              ITimeProvider timeProvider,
                              IAccessKeyStorage accessKeyStorage,
                              IRefreshKeyStorage refreshKeyStorage,
                              IOptions<WindowOptions> options,
                              IUnitOfWork unitOfWork)
        {
            _refreshValidationService = refreshValidationService;
            _sessionQueryService = sessionQueryService;
            _userQueryService = userQueryService;
            _userScopesService = userScopesService;
            _tokenKidService = tokenKidService;
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _timeProvider = timeProvider;
            _accessKeyStorage = accessKeyStorage;
            _refreshKeyStorage = refreshKeyStorage;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        public async Task<TokenPair> RefreshAsync(string refreshToken,
                                                  string newPublicKey,
                                                  long timestamp,
                                                  string signature,
                                                  CancellationToken cancellation = default)
        {
            _refreshValidationService.ValidateWindow(timestamp, _options.Window);

            Guid refreshTokenKid = _tokenKidService.GetTokenKid(refreshToken);
            KeyPair refreshValidationKey = await _refreshKeyStorage.GetKeyPairAsync(refreshTokenKid, cancellation);
            RefreshTokenPayload refreshTokenPayload = _refreshValidationService.ValidateToken(refreshToken, refreshValidationKey);

            Session session = await _sessionQueryService.GetSessionByIdAsync(refreshTokenPayload.SessionId, cancellation);

            _refreshValidationService.ValidateSession(session, refreshTokenPayload);
            _refreshValidationService.ValidateFingerprint(refreshToken, newPublicKey, timestamp, signature, session);

            KeyPair accessPrimaryKey = await _accessKeyStorage.GetPrimaryAsync(cancellation);
            KeyPair refreshPrimaryKey = await _refreshKeyStorage.GetPrimaryAsync(cancellation);

            session.ChangeKidIfNeed(refreshPrimaryKey.Kid);
            session.Update(newPublicKey, _timeProvider.NowUnix());

            User user = await _userQueryService.GetUserByIdAsync(refreshTokenPayload.UserId, cancellation);
            IReadOnlyCollection<Scope> scopes = await _userScopesService.GetUserScopesAsync(user, session.Audience);

            await _unitOfWork.SaveAsync(cancellation);

            return new TokenPair(_accessTokenProvider.Create(new AccessTokenCreationParameters(user, session, scopes), accessPrimaryKey),
                                 _refreshTokenProvider.Create(new RefreshTokenCreationParameters(user, session), refreshPrimaryKey));
        }
    }
}
