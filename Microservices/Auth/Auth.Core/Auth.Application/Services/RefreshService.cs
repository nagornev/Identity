using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Application.Exceptions.Applications.Sessions;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using DDD.Repositories;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class RefreshService : IRefreshService
    {
        private readonly IWindowValidator _windowValidator;

        private readonly IFingerprintValidator _fingerprintValidator;

        private readonly IRefreshTokenValidator _refreshTokenValidator;

        private readonly IRefreshTokenPayloadMapper _refreshTokenMapper;

        private readonly ISessionQueryService _sessionQueryService;

        private readonly IUserQueryService _userQueryService;

        private readonly IUserScopesService _userScopesService;

        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly IKeyStorageReader _accessKeysStorage;

        private readonly IKeyStorageReader _refreshKeysStorage;

        private readonly RefreshOptions _options;

        private readonly IUnitOfWork _unitOfWork;

        public RefreshService(IWindowValidator windowValidator,
                              IFingerprintValidator fingerprintValidator,
                              IRefreshTokenValidator refreshTokenValidator,
                              IRefreshTokenPayloadMapper refreshTokenMapper,
                              ISessionQueryService sessionQueryService,
                              IUserQueryService userQueryService,
                              IUserScopesService userScopesService,
                              IAccessTokenProvider accessTokenProvider,
                              IRefreshTokenProvider refreshTokenProvider,
                              ITimeProvider timeProvider,
                              IAccessKeyStorage accessKeysStorage,
                              IRefreshKeyStorage refreshKeysStorage,
                              IOptions<RefreshOptions> options,
                              IUnitOfWork unitOfWork)
        {
            _windowValidator = windowValidator;
            _fingerprintValidator = fingerprintValidator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenMapper = refreshTokenMapper;
            _sessionQueryService = sessionQueryService;
            _userQueryService = userQueryService;
            _userScopesService = userScopesService;
            _timeProvider = timeProvider;
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _options = options.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenPair> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, string device, string ipAddress, CancellationToken cancellation = default)
        {
            if (_windowValidator.Validate(timestamp, _timeProvider.NowUnix(), _options.Window))
                throw new WindowInvalidApplicationException();

            RefreshTokenPayload refreshTokenPayload = await _refreshTokenMapper.MapAsync(refreshToken);

            if (!await _fingerprintValidator.ValidateAsync(new FingerprintValidationParameters(refreshToken,
                                                                                               newPublicKey,
                                                                                               timestamp,
                                                                                               signature),
                                                            refreshTokenPayload.PublicKey,
                                                            cancellation))
                throw new FingerprintInvalidApplicationException();

            KeyPair refreshValidationKey = await _refreshKeysStorage.GetKeyPairAsync(refreshTokenPayload.Kid, cancellation);

            if (!await _refreshTokenValidator.ValidateAsync(refreshToken, refreshValidationKey, cancellation))
                throw new RefreshTokenInvalidApplicationException();

            Session session = await _sessionQueryService.GetSessionByIdAsync(refreshTokenPayload.SessionId, cancellation);

            if (refreshTokenPayload.Version != session.Version)
                throw new VersionInvalidApplicationException();

            KeyPair accessPrimaryKey = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPair refreshPrimaryKey = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            session.ChangeKidIfNeed(refreshPrimaryKey.Kid);
            session.UpdateSession(device, ipAddress, _timeProvider.NowUnix());

            User user = await _userQueryService.GetUserByIdAsync(refreshTokenPayload.UserId, cancellation);
            IReadOnlyCollection<Scope> scopes = await _userScopesService.GetUserScopesAsync(user, session.Audience);

            await _unitOfWork.SaveAsync(cancellation);

            return new TokenPair(_accessTokenProvider.Create(new AccessTokenCreationParameters(user, session, scopes), accessPrimaryKey),
                                 _refreshTokenProvider.Create(new RefreshTokenCreationParameters(user, session, newPublicKey), refreshPrimaryKey));
        }
    }
}
