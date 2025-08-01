using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Validators
{
    public interface IWindowValidator
    {
        bool Validate(long timestamp, long now, int window);
    }
}
