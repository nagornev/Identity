using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class EmailTokenOptions : TokenOptions
    {
        public EmailTokenOptions(int lifetime) 
            : base(lifetime)
        {
        }
    }
}
