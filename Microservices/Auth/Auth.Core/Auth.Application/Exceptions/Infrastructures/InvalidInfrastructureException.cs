using Auth.Application.Exceptions.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Infrastructures
{
    public class InvalidInfrastructureException : InfrastructureException
    {
        public InvalidInfrastructureException(string message, Exception? inner = null)
            : base(InfrastructuireErrorTypes.Invalid, message, inner)
        {
        }
    }
}
