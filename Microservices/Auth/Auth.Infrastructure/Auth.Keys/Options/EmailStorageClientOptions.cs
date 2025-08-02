using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Options
{
    public class EmailStorageClientOptions : VaultStorageClientOptions
    {
        public EmailStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
