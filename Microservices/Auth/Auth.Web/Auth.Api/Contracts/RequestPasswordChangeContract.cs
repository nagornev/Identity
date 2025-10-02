namespace Auth.Api.Contracts
{
    public class RequestPasswordChangeContract
    {
        public RequestPasswordChangeContract(string oldPassword,
                                             string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string OldPassword { get; }
        public string NewPassword { get; }
    }
}
