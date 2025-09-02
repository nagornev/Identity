namespace Auth.Api.Contracts
{
    public class SignUpContract
    {
        public SignUpContract(string emailAddress,
                              string password,
                              string personName)
        {
            EmailAddress = emailAddress;
            Password = password;
            PersonName = personName;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public string PersonName { get; }
    }
}
