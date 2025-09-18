using DDD.Primitives;
using Otp.Domain.Events;
using Otp.Domain.Exceptions.Domains.OneTimePasswords;
using Otp.Domain.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace Otp.Domain.Aggregates
{
    public partial class OneTimePassword : AggregateRoot
    {
        private const int _maximumAttempts = 3;

        private OneTimePassword(Guid id, Guid userId, Channel channel, Secret secret, string tag, string payload, long createdAt, long expiresAt)
        {
            Id = id;
            UserId = userId;
            Channel = channel;
            Secret = secret;
            Tag = tag;
            Payload = payload;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public static OneTimePassword Create(Guid userId, Channel channel, string secret, string tag, long createdAt, long expiresAt, string payload = "")
        {
            Guid id = Guid.NewGuid();

            OneTimePassword oneTimePassword = new OneTimePassword(id,
                                                                  userId,
                                                                  channel,

                                                                  Secret.Create(secret) ??
                                                                  throw new SecretNullDomainException(),

                                                                  tag,
                                                                  payload,
                                                                  createdAt,
                                                                  expiresAt);

            oneTimePassword.AddDomainEvent(new OneTimePasswordCreatedDomainEvent(oneTimePassword.Id));

            return oneTimePassword;
        }
        public Guid UserId { get; private set; }

        public Channel Channel { get; private set; }

        public Secret Secret { get; private set; }

        public string Tag { get; private set; }

        public string? Payload { get; private set; }

        public ushort Attempts { get; private set; }

        public long CreatedAt { get; private set; }

        public long ExpiresAt { get; private set; }

        public bool Used { get; private set; }

        public bool Deleted { get; private set; }

        public string GetValue(int digits = 6)
        {
            byte[] key = Secret.DecodeToBytes();
            string message = $"{UserId}{Payload}{CreatedAt}";

            using var hmac = new HMACSHA256(key);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

            int offset = hash[^1] & 0x0F;
            int binary =
                ((hash[offset] & 0x7f) << 24) |
                ((hash[offset + 1] & 0xff) << 16) |
                ((hash[offset + 2] & 0xff) << 8) |
                (hash[offset + 3] & 0xff);

            int otp = binary % (int)Math.Pow(10, digits);

            return otp.ToString(new string('0', digits));
        }

        public bool ValidateAt(string oneTimePasswordValue, string tag, long timestamp)
        {
            if (Attempts >= _maximumAttempts ||
                timestamp > ExpiresAt ||
                Tag != tag ||
                Used)
                return false;

            bool result = CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(oneTimePasswordValue),
                                                                  Encoding.UTF8.GetBytes(GetValue()));

            if (!result)
                Attempts++;
            else
            {
                Used = true;
                AddDomainEvent(new OneTimePasswordUsedDomainEvent(Id));
            }

            return result;
        }

        public void Resend(string secret, long timestamp)
        {
            if (timestamp > ExpiresAt)
                throw new OneTimePasswordInvalidDomainException();

            Secret = Secret.Create(secret) ??
                     throw new SecretNullDomainException();

            AddDomainEvent(new OneTimePasswordResendedDomainEvent(Id));
        }

        public void MarkAsDeleted()
        {
            if (Deleted)
                throw new OneTimePasswordAlreadyDeletedDomainException();

            Deleted = true;

            AddDomainEvent(new OneTimePasswordDeletedDomainEvent(Id, UserId, Tag, CreatedAt, Used));
        }

    }

    #region Ef

    public partial class OneTimePassword
    {
        private OneTimePassword()
        {

        }
    }

    #endregion
}
