using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Options
{
    public class AccessStorageClientOptions : VaultStorageClientOptions
    {
        public AccessStorageClientOptions(string token, string address) : base(token, address)
        {
        }
    }
}
