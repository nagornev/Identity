namespace Auth.Api.Contracts
{
    public class RequestUserSignUpContract
    {
        public RequestUserSignUpContract(string emailAddress,
                                         string personName,
                                         string password)
        {
            EmailAddress = emailAddress;
            PersonName = personName;
            Password = password;
        }

        public string EmailAddress { get; }

        public string PersonName { get; }

        public string Password { get; }
    }
}
