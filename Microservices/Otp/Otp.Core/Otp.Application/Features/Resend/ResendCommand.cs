namespace Otp.Application.Features.Resend
{
    public class ResendCommand : ResultRequest
    {
        public ResendCommand(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
