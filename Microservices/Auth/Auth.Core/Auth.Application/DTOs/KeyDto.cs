namespace Auth.Application.DTOs
{
    public class KeyDto
    {
        public KeyDto(Guid kid,
                      string algorithm,
                      byte[] key)
        {
            Kid = kid;
            Key = key;
        }

        public Guid Kid { get; }

        public string Algorithm { get; }

        public byte[] Key { get; }
    }
}
