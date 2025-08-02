using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public class FingerprintValidationParameters
    {
        public FingerprintValidationParameters(string refreshToken,
                                               string newPublicKey,
                                               long timestamp,
                                               string signature)
        {
            RefreshToken = refreshToken;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
        }

        public string RefreshToken { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }
    }
}
