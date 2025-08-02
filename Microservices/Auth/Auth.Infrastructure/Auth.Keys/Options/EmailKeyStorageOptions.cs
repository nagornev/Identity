using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Options
{
    public class EmailKeyStorageOptions : VaultKeyStorageOptions
    {
        public EmailKeyStorageOptions(string primaryKey,
                                       string basePath)
            : base(primaryKey, basePath)
        {
        }
    }
}
