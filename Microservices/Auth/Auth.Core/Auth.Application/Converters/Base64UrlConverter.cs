namespace Auth.Application.Converters
{
    public static class Base64UrlConverter
    {
        public static byte[] FromBase64Url(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }

        public static string ToBase64Url(byte[] bytes)
        {
            string base64 = Convert.ToBase64String(bytes);
            string base64Url = base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
            return base64Url;
        }
    }
}
