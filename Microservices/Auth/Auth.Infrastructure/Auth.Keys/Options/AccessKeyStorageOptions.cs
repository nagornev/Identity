using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Options
{
    public class AccessKeyStorageOptions : KeyStorageOptions
    {
        public AccessKeyStorageOptions(string token, string address, string basePath) : base(token, address, basePath)
        {
        }
    }
}
