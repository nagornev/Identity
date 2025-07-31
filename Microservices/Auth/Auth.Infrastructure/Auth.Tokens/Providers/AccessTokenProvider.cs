using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Tokens.Providers
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        public AccessTokenProvider()
        {
            
        }

        public string Create(KeyDto accessPrivateKey, User user, Session session)
        {
            throw new NotImplementedException();
        }
    }
}
