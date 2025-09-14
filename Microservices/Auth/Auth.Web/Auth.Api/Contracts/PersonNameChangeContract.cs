namespace Auth.Api.Contracts
{
    public class PersonNameChangeContract
    {
        public PersonNameChangeContract(string newPersonName)
        {
            NewPersonName = newPersonName;
        }

        public string NewPersonName { get; }
    }
}
