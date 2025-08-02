using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignInConfrimService : ISignInConfirmService
    {
        private readonly IOtpAuthenticationService _otpAuthenticationService;

        private readonly IUserScopesService _userScopesService;

        private readonly IKeyStorageReader _accessKeysStorage;

        private readonly IKeyStorageReader _refreshKeysStorage;

        private readonly ISessionFactory _sessionFactory;

        private readonly IRepositoryWriter<Session> _sessionRepository;

        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        private readonly IUnitOfWork _unitOfWork;

        public SignInConfrimService(IOtpAuthenticationService otpAuthenticationService,
                                    IUserScopesService userScopesService,
                                    IAccessKeyStorage accessKeysStorage,
                                    IRefreshKeyStorage refreshKeysStorage,
                                    ISessionFactory sessionFactory,
                                    IRepositoryWriter<Session> sessionRepository,
                                    IAccessTokenProvider accessTokenProvider,
                                    IRefreshTokenProvider refreshTokenProvider,
                                    IUnitOfWork unitOfWork)
        {
            _otpAuthenticationService = otpAuthenticationService;
            _userScopesService = userScopesService;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _sessionFactory = sessionFactory;
            _sessionRepository = sessionRepository;
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenPair> ConfirmAsync(string otpToken, string otp, string audience, string publicKey, string device, string ipAddress, CancellationToken cancellation = default)
        {
            User user = await _otpAuthenticationService.AuthenticateAsync(otpToken, otp, OtpTags.SignIn, cancellation);

            KeyPair accessKeyPair = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPair refreshKeyPair = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            Session session = _sessionFactory.Create(user.Id, refreshKeyPair.Kid, audience, device, ipAddress);
            await _sessionRepository.AddAsync(session, cancellation);

            IReadOnlyCollection<Scope> scopes = await _userScopesService.GetUserScopesAsync(user, session.Audience);

            await _unitOfWork.SaveAsync(cancellation);

            return new TokenPair(_accessTokenProvider.Create(new AccessTokenCreationParameters(user, session, scopes), accessKeyPair),
                                 _refreshTokenProvider.Create(new RefreshTokenCreationParameters(user, session, publicKey), refreshKeyPair));
        }
    }
}
