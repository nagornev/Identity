using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Storages.Keys;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignInConfrimService : ISignInConfirmService
    {
        private readonly IOtpAuthenticationService _otpAuthenticationService;

        private readonly IKeysStorageReader _accessKeysStorage;

        private readonly IKeysStorageReader _refreshKeysStorage;

        private readonly ISessionFactory _sessionFactory;

        private readonly IRepositoryWriter<Session> _sessionRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthProvider _authProvider;

        public SignInConfrimService(IOtpAuthenticationService otpAuthenticationService,
                                    IAccessKeysStorage accessKeysStorage,
                                    IRefreshKeysStorage refreshKeysStorage,
                                    ISessionFactory sessionFactory,
                                    IRepositoryWriter<Session> sessionRepository,
                                    IUnitOfWork unitOfWork,
                                    IAuthProvider authProvider)
        {
            _otpAuthenticationService = otpAuthenticationService;
            _accessKeysStorage = accessKeysStorage;
            _refreshKeysStorage = refreshKeysStorage;
            _sessionFactory = sessionFactory;
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _authProvider = authProvider;
        }

        public async Task<AuthDto> ConfirmAsync(string otpToken, string otp, string publicKey, string device, string ipAddress, CancellationToken cancellation = default)
        {
            User user = await _otpAuthenticationService.AuthenticateAsync(otpToken, otp, OtpTags.SignIn, cancellation);

            KeyDto accessPrivateKey = await _accessKeysStorage.GetPrivateKeyAsync(cancellation);
            KeyDto refreshPrivateKey = await _refreshKeysStorage.GetPrivateKeyAsync(cancellation);

            Session session = _sessionFactory.Create(user.Id, refreshPrivateKey.Kid, device, ipAddress);
            await _sessionRepository.AddAsync(session, cancellation);

            await _unitOfWork.SaveAsync(cancellation);

            return _authProvider.Create(accessPrivateKey, refreshPrivateKey, user, session, publicKey);
        }
    }
}
