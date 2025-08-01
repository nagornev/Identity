using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class EmailKeyOptions : KeyOptions
    {
        public EmailKeyOptions(int size,
                               int timeToLive,
                               int rotationInterval)
            : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
