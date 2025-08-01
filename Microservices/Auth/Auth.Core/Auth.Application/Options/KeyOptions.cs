using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public abstract class KeyOptions
    {
        public KeyOptions(int size,
                          int timeToLive,
                          int rotationInterval)
        {
            Size = size;
            TimeToLive = timeToLive;
            RotationInterval = rotationInterval;
        }

        public int Size { get; }

        public int TimeToLive { get; }

        public int RotationInterval { get; }
    }
}
