using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Storages.Keys;
using Auth.Application.Abstractions.Validators;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class RefreshService : IRefreshService
    {
        private const int _refreshWindow = 5000;

        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshSignatureValidator _refreshSignatureValidator;
        private readonly IRefreshTokenMapper _refreshTokenMapper;

        private readonly ISessionQueryService _sessionQueryService;
        private readonly ITimeProvider _timeProvider;
        private readonly IKeysStorageReader _accessKeysStorage;
        private readonly IKeysStorageReader _refreshKeysStorage;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthProvider _authProvider;

        public RefreshService(IRefreshTokenValidator refreshTokenValidator,
                              IRefreshSignatureValidator refreshSignatureValidator,
                              IRefreshTokenMapper refreshTokenMapper,
                              ISessionQueryService sessionQueryService,
                              ITimeProvider timeProvider,
                              IAccessKeysStorage accessKeysStorage,
                              IRefreshKeysStorage refreshKeysStorage,
                              IUnitOfWork unitOfWork,
                              IAuthProvider authProvider)
        {
            _refreshTokenValidator = refreshTokenValidator;
            _refreshSignatureValidator = refreshSignatureValidator;
            _refreshTokenMapper = refreshTokenMapper;
            _sessionQueryService = sessionQueryService;
            _timeProvider = timeProvider;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _unitOfWork = unitOfWork;
            _authProvider = authProvider;
        }

        public async Task<AuthDto> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, string device, string ipAddress, CancellationToken cancellation = default)
        {
            if (_timeProvider.NowUnix() - timestamp > _refreshWindow)
                throw new InvalidOperationException();

            if (!await _refreshSignatureValidator.ValidateAsync(refreshToken, newPublicKey, timestamp, signature, cancellation))
                throw new InvalidOperationException();

            if (!await _refreshTokenValidator.ValidateAsync(refreshToken, cancellation))
                throw new InvalidOperationException();

            RefreshTokenDto refreshTokenDto = await _refreshTokenMapper.MapAsync(refreshToken);
            Session session = await _sessionQueryService.GetSessionByIdAsync(refreshTokenDto.SessionId, cancellation);

            if (refreshTokenDto.Version != session.Version)
                throw new InvalidOperationException();

            KeyDto accessPrivateKey = await _accessKeysStorage.GetPrivateKeyAsync(cancellation);
            KeyDto refreshPrivateKey = await _refreshKeysStorage.GetPrivateKeyAsync(cancellation);

            if (refreshPrivateKey.Kid != session.Kid)
                session.ChangeKid(refreshPrivateKey.Kid);

            session.UpdateSession(device, ipAddress, _timeProvider.NowUnix());

            await _unitOfWork.SaveAsync(cancellation);

            return _authProvider.Create(accessPrivateKey, refreshPrivateKey, user, session, newPublicKey);
        }
    }
}
