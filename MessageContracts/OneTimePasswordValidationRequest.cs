namespace MessageContracts
{
    public class OneTimePasswordValidationRequest : IRequestMessageContract
    {
        public OneTimePasswordValidationRequest(Guid oneTimePasswordId,
                                    string oneTimePasswordValue,
                                    string tag)
        {
            OneTimePasswordId = oneTimePasswordId;
            OneTimePasswordValue = oneTimePasswordValue;
            Tag = tag;
        }

        public Guid OneTimePasswordId { get; }

        public string OneTimePasswordValue { get; }

        public string Tag { get; }
    }
}
