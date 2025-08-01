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

        private readonly IKeyStorageReader _accessKeysStorage;

        private readonly IKeyStorageReader _refreshKeysStorage;

        private readonly ISessionFactory _sessionFactory;

        private readonly IRepositoryWriter<Session> _sessionRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthProvider _authProvider;

        public SignInConfrimService(IOtpAuthenticationService otpAuthenticationService,
                                    IAccessKeyStorage accessKeysStorage,
                                    IRefreshKeyStorage refreshKeysStorage,
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

            KeyPairDto accessKeyPair = await _accessKeysStorage.GetPrimaryAsync(cancellation);
            KeyPairDto refreshKeyPair = await _refreshKeysStorage.GetPrimaryAsync(cancellation);

            Session session = _sessionFactory.Create(user.Id, refreshKeyPair.Kid, device, ipAddress);
            await _sessionRepository.AddAsync(session, cancellation);

            await _unitOfWork.SaveAsync(cancellation);

            return _authProvider.Create(accessKeyPair, refreshKeyPair, user, session, publicKey);
        }
    }
}
