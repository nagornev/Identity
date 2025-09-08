using Auth.Application.Abstractions.Providers;
using System.Text.Json;

namespace Auth.Security.Providers
{
    public class OtpTokenPayloadProvider : IOtpTokenPayloadProvider
    {
        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public T Deserialize<T>(string payload)
        {
            return JsonSerializer.Deserialize<T>(payload)!;
        }
    }
}
