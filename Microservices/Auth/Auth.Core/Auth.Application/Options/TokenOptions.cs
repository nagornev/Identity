using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public abstract class TokenOptions
    {
        public TokenOptions(string issuer,
                            string audience,
                            int lifetime)
        {
            Issuer = issuer;
            Audience = audience;
            Lifetime = lifetime;
        }

        public string Issuer { get; }

        public string Audience { get; }

        public int Lifetime { get; }
    }
}
