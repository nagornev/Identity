using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Options
{
    public class RefreshOptions
    {
        public RefreshOptions(int window)
        {
            Window = window;
        }

        public int Window { get; }
    }
}
