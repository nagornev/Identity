using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Security.Options
{
    public class PasswordHashOptions
    {
        public PasswordHashOptions(string salt)
        {
            Salt = salt;
        }

        public string Salt { get; }
    }
}
