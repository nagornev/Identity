namespace Auth.Application.DTOs
{
    public class KeyDto
    {
        public KeyDto(Guid kid,
                      byte[] key)
        {
            Kid = kid;
            Key = key;
        }

        public Guid Kid { get; }

        public byte[] Key { get; }
    }
}
