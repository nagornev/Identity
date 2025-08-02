namespace Auth.Application.Features.Refresh
{
    public class RefreshCommand : ResultTRequest<DTOs.TokenPair>
    {
        public RefreshCommand(string refreshToken,
                              string newPublicKey,
                              long timestamp,
                              string signature,
                              string device,
                              string ipAddress)
        {
            RefreshToken = refreshToken;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
            Device = device;
            IpAddress = ipAddress;
        }

        public string RefreshToken { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }

        public string Device { get; }

        public string IpAddress { get; }
    }
}
