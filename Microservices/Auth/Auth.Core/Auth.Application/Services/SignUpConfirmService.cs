using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignUpConfirmService : ISignUpConfirmService
    {
        private readonly ITokenKidService _tokenKidService;

        private readonly IChannelKeyStorage _channelKeyStorage;

        private readonly IChannelTokenValidationService _channelTokenValidationService;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;


        public SignUpConfirmService(ITokenKidService tokenKidService,
                                    IChannelKeyStorage channelKeyStorage,
                                    IChannelTokenValidationService channelTokenValidationService,
                                    IUserQueryService userQueryService,
                                    IUnitOfWork unitOfWork)
        {
            _tokenKidService = tokenKidService;
            _channelKeyStorage = channelKeyStorage;
            _channelTokenValidationService = channelTokenValidationService;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string channelToken, CancellationToken cancellation = default)
        {
            Guid channelTokenKid = _tokenKidService.GetTokenKid(channelToken);
            KeyPair channelValidationKey = await _channelKeyStorage.GetKeyPairAsync(channelTokenKid, cancellation);

            ChannelTokenPayload channelTokenPayload = _channelTokenValidationService.Validate(channelToken, channelValidationKey, ChannelTags.Email);

            User user = await _userQueryService.GetUserByIdAsync(channelTokenPayload.UserId, cancellation);
            user.Activate();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
