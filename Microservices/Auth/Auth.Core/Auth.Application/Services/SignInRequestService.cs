using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignInRequestService : ISignInRequestService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUserValidator _userValidator;

        private readonly IPasswordValidator _passwordValidator;

        private readonly IRefreshKeyStorage _refreshKeyStorage;

        private readonly ISessionFactory _sessionFactory;

        private readonly IRepositoryWriter<Session> _sessionRepository;

        private readonly IOtpTokenPayloadProvider _otpPayloadMapper;

        private readonly IOtpClient _otpClient;

        private readonly IUnitOfWork _unitOfWork;

        public SignInRequestService(IUserQueryService userQueryService,
                                    IUserValidator userValidator,
                                    IPasswordValidator passwordValidator,
                                    IRefreshKeyStorage refreshKeyStorage,
                                    ISessionFactory sessionFactory,
                                    IRepositoryWriter<Session> sessionRepository,
                                    IOtpTokenPayloadProvider otpPayloadMapper,
                                    IOtpClient otpClient,
                                    IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _refreshKeyStorage = refreshKeyStorage;
            _sessionFactory = sessionFactory;
            _sessionRepository = sessionRepository;
            _otpPayloadMapper = otpPayloadMapper;
            _otpClient = otpClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<Otp> RequestAsync(string emailAddress,
                                             string password,
                                             string audience,
                                             string publicKey,
                                             RequestContext requestContext,
                                             CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByEmailAsync(emailAddress, cancellation);

            if (!_userValidator.Validate(user))
                throw new UserInvalidApplicationException(emailAddress);

            if (!_passwordValidator.Verify(password, user.Authentication.PasswordHash.Value, user.Authentication.PasswordSalt))
                throw new UserInvalidPasswordApplicationException(emailAddress);

            KeyPair refreshPrimaryKey = await _refreshKeyStorage.GetPrimaryAsync(cancellation);

            Session session = _sessionFactory.Create(user.Id, refreshPrimaryKey.Kid, publicKey, audience, requestContext.Device, requestContext.IpAddress);
            await _sessionRepository.AddAsync(session, cancellation);

            Otp otp = await _otpClient.CreateAsync(user.Id,
                                                   OtpTags.SignIn,
                                                   payload: _otpPayloadMapper.Serialize(new SignInOtpTokenPayload(session.Id)),
                                                   cancellation: cancellation);

            await _unitOfWork.SaveAsync(cancellation);

            return otp;
        }
    }
}
