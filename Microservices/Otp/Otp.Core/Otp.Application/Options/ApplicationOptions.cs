namespace Otp.Application.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions(string issuer,
                                  string audience,
                                  string jwksUrl)
        {
            Issuer = issuer;
            Audience = audience;
            JwksUrl = jwksUrl;
        }

        public string Issuer { get; }

        public string Audience { get; }

        public string JwksUrl { get; }
    }
}
