namespace Auth.Api.Contracts
{
    public class RequestEmailAddressChangeContract
    {
        public RequestEmailAddressChangeContract(string newEmailAddress)
        {
            NewEmailAddress = newEmailAddress;
        }

        public string NewEmailAddress { get; }
    }
}
