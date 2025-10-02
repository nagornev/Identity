using Auth.Application.DTOs;

namespace Auth.Application.Features.ChangePassword.Commands
{
    public class RequestPasswordChangeCommand : ResultTRequest<Otp>
    {
        public RequestPasswordChangeCommand(Guid userId, string oldPassword, string newPassword)
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public Guid UserId { get; }

        public string OldPassword { get; }

        public string NewPassword { get; }
    }
}
