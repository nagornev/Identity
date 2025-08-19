using Auth.Application.Abstractions.Providers;
using System.Text;

namespace Auth.Application.Providers
{
    public class FingerprintMessageProvider : IFingerprintMessageProvider
    {
        public string GetMessage(params object[] tokens)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (object token in tokens)
            {
                stringBuilder.Append(token.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
