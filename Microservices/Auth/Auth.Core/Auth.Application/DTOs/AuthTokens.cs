namespace Auth.Application.DTOs
{
    public class AuthTokens
    {
        public AuthTokens(string accessToken,
                          string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }

        public string RefreshToken { get; }
    }
}
