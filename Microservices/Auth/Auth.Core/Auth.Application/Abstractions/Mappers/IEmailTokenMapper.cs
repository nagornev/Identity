using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Mappers
{
    public interface IEmailTokenMapper : ITokenMapper<EmailTokenDto>
    {
    }
}
