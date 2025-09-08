using Otp.Domain.Aggregates;

namespace Otp.Application.Abstractions.Factories
{
    public interface IOneTimePasswordFactory
    {
        OneTimePassword Create(string tag, Guid subject, string? payload = "");
    }
}
