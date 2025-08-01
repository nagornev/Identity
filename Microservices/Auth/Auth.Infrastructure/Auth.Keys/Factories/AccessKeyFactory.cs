using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Tokens.Abstractions.Factories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Factories
{
    public class AccessKeyFactory : RsaKeyFactory<AccessKeyOptions>, IAccessKeyFactory
    {
        public AccessKeyFactory(IOptions<AccessKeyOptions> options,
                                ITimeProvider timeProvider) 
            : base(options, timeProvider)
        {
        }
    }
}
