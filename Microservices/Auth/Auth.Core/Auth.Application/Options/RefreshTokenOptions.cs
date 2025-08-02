using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class RefreshTokenOptions : TokenOptions
    {
        public RefreshTokenOptions(int lifetime) 
            : base(lifetime)
        {
        }
    }
}
