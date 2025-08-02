using Auth.Domain.Exceptions.Domains.Scopes;
using Auth.Domain.Exceptions.Domains.Sessions;
using DDD.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.ValueObjects
{
    public class Audience : ValueObject
    {
        private Audience(string value)
        {
            
            Value = value;
        }

        internal static Audience Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new AudienceEmptyDomainException();

            return new Audience(value);
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
