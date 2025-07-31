using Auth.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Factories
{
    public interface ISessionFactory
    {
        Session Create(Guid userId, Guid kid, string device, string ipAddress);
    }
}
