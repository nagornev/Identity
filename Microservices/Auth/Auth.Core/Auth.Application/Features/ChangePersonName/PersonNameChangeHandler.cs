using Auth.Application.Abstractions.Services;

namespace Auth.Application.Features.ChangePersonName
{
    public class PersonNameChangeHandler : ResultRequestHandler<PersonNameChangeCommand>
    {
        private readonly IPersonNameService _personNameService;

        public PersonNameChangeHandler(IPersonNameService personNameService)
        {
            _personNameService = personNameService;
        }

        public override async Task HandleAsync(PersonNameChangeCommand request, CancellationToken cancellation)
        {
            await _personNameService.ChangeAsync(request.UserId, request.PersonName, cancellation);
        }
    }
}
