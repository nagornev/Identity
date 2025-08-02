namespace Auth.Application.DTOs
{
    public class TokenPair
    {
        public TokenPair(string accessToken,
                         string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }

        public string RefreshToken { get; }
    }
}
