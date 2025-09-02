using Auth.Application.Abstractions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
