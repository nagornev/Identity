using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Infrastructures
{
    public class CanceledInfrastructureException : InfrastructureException
    {
        public CanceledInfrastructureException(string message, Exception? inner = null) 
            : base(InfrastructuireErrorTypes.Canceled, message, inner)
        {
        }
    }
}
