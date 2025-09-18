using DDD.Primitives;
using Notification.Domain.Consts;
using Notification.Domain.Exceptions.Domains.Notifications;

namespace Notification.Domain.ValueObjects
{
    public partial class Channel : ValueObject
    {
        private Channel(string type, string value)
        {
            Type = type;
            Value = value;
        }

        internal static Channel Create(string type, string value)
        {
            if (string.IsNullOrEmpty(type) ||
                string.IsNullOrWhiteSpace(type))
                throw new ChannelTypeEmptyDomainException();

            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new ChannelValueEmptyDomainException();

            return new Channel(type, value);
        }

        internal static Channel CreateEmailAddressChannel(string email)
        {
            return Create(ChannelType.Email, email);
        }

        internal static Channel CreatePhoneChannel(string phone)
        {
            return Create(ChannelType.Sms, phone);
        }

        public string Type { get; private set; }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }
    }

    #region Ef
    public partial class Channel
    {
        private Channel()
        {
        }
    }
    #endregion
}
