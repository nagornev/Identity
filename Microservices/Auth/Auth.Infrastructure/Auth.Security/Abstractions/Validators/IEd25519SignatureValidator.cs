namespace Auth.Security.Abstractions.Validators
{
    public interface IEd25519SignatureValidator
    {
        bool Validate(byte[] data, byte[] publicKey, byte[] signature);
    }
}
