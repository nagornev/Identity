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
        public ApplicationOptions(string basicRoleName)
        {
            BasicRoleName = basicRoleName;
        }

        public string BasicRoleName { get; }
    }
}
