namespace Auth.Application.Abstractions.Validators
{
    public interface IFingerprintValidator
    {
        Task<bool> ValidateAsync(string refreshToken, string newPublicKey, long timestamp, string signature, CancellationToken cancellation = default);
    }
}
