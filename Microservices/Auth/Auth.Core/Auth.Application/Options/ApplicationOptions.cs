using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions(string issuer, string basicRoleName)
        {
            Issuer = issuer;
            BasicRoleName = basicRoleName;
        }

        public string Issuer { get; }

        public string BasicRoleName { get; }
    }
}
