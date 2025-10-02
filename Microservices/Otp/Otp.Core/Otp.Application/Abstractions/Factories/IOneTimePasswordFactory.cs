using Otp.Domain.Aggregates;
using Otp.Domain.ValueObjects;

namespace Otp.Application.Abstractions.Factories
{
    public interface IOneTimePasswordFactory
    {
        OneTimePassword Create(Guid userId, Channel channel, string tag, string? payload = "");
    }
}
