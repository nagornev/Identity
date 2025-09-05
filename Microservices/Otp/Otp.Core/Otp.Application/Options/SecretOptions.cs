using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Options
{
    public class SecretOptions
    {
        public SecretOptions(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }
}
