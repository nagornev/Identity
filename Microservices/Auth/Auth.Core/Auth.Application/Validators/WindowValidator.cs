using Auth.Application.Abstractions.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validators
{
    public class WindowValidator : IWindowValidator
    {
        public bool Validate(long timestamp, long now, int window)
        {
            return now - timestamp < window;
        }
    }
}
