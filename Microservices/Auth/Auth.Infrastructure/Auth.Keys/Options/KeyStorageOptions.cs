using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Options
{
    public abstract class KeyStorageOptions
    {
        public KeyStorageOptions(string token, string address, string basePath)
        {
            Token = token;
            Address = address;
            BasePath = basePath;
        }

        public string Token { get; }

        public string Address { get; }

        public string BasePath { get; }
    }
}
