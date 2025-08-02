using Auth.Application.Abstractions.Providers;

namespace Auth.Application.Providers
{
    public class FingerprintFormatProvider : IFingerprintFormatProvider
    {
        public string GetFormat(string refrehToken, string newPublicKey, long timestamp)
        {
            return $"{refrehToken}.{newPublicKey}.{timestamp}";
        }
    }
}
