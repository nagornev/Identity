using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class RefreshKeyOptions : KeyOptions
    {
        public RefreshKeyOptions(int size,
                                 int timeToLive,
                                 int rotationInterval) 
            : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
