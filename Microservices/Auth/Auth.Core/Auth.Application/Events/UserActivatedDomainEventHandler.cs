using Auth.Application.Abstractions.Events;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Events;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Events
{
    public class UserActivatedDomainEventHandler : IDomainEventHandler<UserActivatedDomainEvent>
    {
        private readonly IPublishMessageService _publishMessageService;

        public UserActivatedDomainEventHandler(IPublishMessageService publishMessageService)
        {
            _publishMessageService = publishMessageService;
        }

        public async Task HandleAsync(UserActivatedDomainEvent domainEvent)
        {
            UserActivatedMessageContract messageContract = new UserActivatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress);

            await _publishMessageService.PublishAsync(messageContract);
        }
    }
}
