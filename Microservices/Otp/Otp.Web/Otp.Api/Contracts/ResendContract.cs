namespace Otp.Api.Contracts
{
    public class ResendContract
    {
        public ResendContract(Guid oneTimePasswordId)
        {
            OneTimePasswordId = oneTimePasswordId;
        }

        public Guid OneTimePasswordId { get; }
    }
}
