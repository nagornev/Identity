namespace MessageContracts
{
    public class OneTimePasswordNotificationMessageContract
    {
        public OneTimePasswordNotificationMessageContract(Guid subject,
                                                          string oneTimePasswordValue)
        {
            Subject = subject;
            OneTimePasswordValue = oneTimePasswordValue;
        }

        public Guid Subject { get; }

        public string OneTimePasswordValue { get; }
    }
}
