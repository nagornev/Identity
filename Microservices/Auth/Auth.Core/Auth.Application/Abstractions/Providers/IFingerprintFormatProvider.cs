using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Providers
{
    public interface IFingerprintFormatProvider
    {
        string GetFormat(string refrehToken, string newPublicKey, long timestamp);
    }
}
