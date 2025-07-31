using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions
{
    internal class DomainErrorTypes
    {
        public const int Null = 1;

        public const int InvalidFomat = 2;

        public const int Empty = 3;

        public const int NotFound = 4;

        public const int Already = 5;

        public const int OutOfRange = 6;
    }
}
