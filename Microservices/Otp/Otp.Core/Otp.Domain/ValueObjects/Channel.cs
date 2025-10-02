using DDD.Primitives;
using Otp.Domain.Consts;
using Otp.Domain.Exceptions.Domains.Users;

namespace Otp.Domain.ValueObjects
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

        public string Mask()
        {
            return Type switch
            {
                ChannelType.Email => MaskEmail(Value),
                ChannelType.Sms => MaskPhone(Value),
                _ => "***"
            };
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }

        private static string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return "***";

            var parts = email.Split('@');
            var localPart = parts[0];
            var domainPart = parts[1];

            var visible = localPart.Length > 2 ? localPart.Substring(0, 2) : localPart.Substring(0, 1);
            return $"{visible}***@{domainPart}";
        }

        private static string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 4)
                return "***";

            return new string('*', phone.Length - 4) + phone[^4..];
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
