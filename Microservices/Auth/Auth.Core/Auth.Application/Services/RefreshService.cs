using Auth.Application.Abstractions.Mappers;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Storages.Keys;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Sessions;
using Auth.Application.Exceptions.Applications.Tokens;
using Auth.Domain.Aggregates;
using DDD.Repositories;
using System.Runtime.CompilerServices;

namespace Auth.Application.Services
{
    public class RefreshService : IRefreshService
    {
        private const int _refreshWindow = 5000;

        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly ISignatureValidator _signatureValidator;
        private readonly IRefreshTokenMapper _refreshTokenMapper;

        private readonly ISessionQueryService _sessionQueryService;
        private readonly IUserQueryService _userQueryService;
        private readonly ITimeProvider _timeProvider;
        private readonly IKeyStorageReader _accessKeysStorage;
        private readonly IKeyStorageReader _refreshKeysStorage;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthProvider _authProvider;

        public RefreshService(IRefreshTokenValidator refreshTokenValidator,
                              ISignatureValidator signatureValidator,
                              IRefreshTokenMapper refreshTokenMapper,
                              ISessionQueryService sessionQueryService,
                              IUserQueryService userQueryService,
                              ITimeProvider timeProvider,
                              IAccessKeyStorage accessKeysStorage,
                              IRefreshKeyStorage refreshKeysStorage,
                              IUnitOfWork unitOfWork,
                              IAuthProvider authProvider)
        {
            _refreshTokenValidator = refreshTokenValidator;
            _signatureValidator = signatureValidator;
            _refreshTokenMapper = refreshTokenMapper;
            _sessionQueryService = sessionQueryService;
            _userQueryService = userQueryService;
            _timeProvider = timeProvider;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _unitOfWork = unitOfWork;
            _authProvider = authProvider;
        }

        public async Task<AuthDto> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, string device, string ipAddress, CancellationToken cancellation = default)
        {
            if (_timeProvider.NowUnix() - timestamp > _refreshWindow)
                throw new RefreshWindowInvalidApplicationException();

            if (!await _refreshTokenValidator.ValidateAsync(refreshToken, cancellation))
                throw new RefreshTokenInvalidApplicationException();

            if (!await _signatureValidator.ValidateAsync(refreshToken, newPublicKey, timestamp, signature, cancellation))
                throw new SignatureInvalidApplicationException();

            RefreshTokenDto refreshTokenDto = await _refreshTokenMapper.MapAsync(refreshToken);
            Session session = await _sessionQueryService.GetSessionByIdAsync(refreshTokenDto.SessionId, cancellation);

            if (refreshTokenDto.Version != session.Version)
                throw new VersionInvalidApplicationException();

            User user = await _userQueryService.GetUserByIdAsync(refreshTokenDto.UserId, cancellation);

            KeyPairDto accessKeyPair = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPairDto refreshKeyPair = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            session.ChangeKidIfNeed(refreshKeyPair.Kid);
            session.UpdateSession(device, ipAddress, _timeProvider.NowUnix());

            await _unitOfWork.SaveAsync(cancellation);

            return _authProvider.Create(accessKeyPair, refreshKeyPair, user, session, newPublicKey);
        }
    }
}
