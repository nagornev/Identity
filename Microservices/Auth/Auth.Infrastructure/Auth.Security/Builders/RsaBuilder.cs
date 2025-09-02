using System.Security.Cryptography;

namespace Auth.Security.Builders
{
    internal static class RsaBuilder
    {
        public static void CreateKeys(int size, out byte[] privateKey, out byte[] publicKey)
        {
            using (RSA rsa = RSA.Create(size))
            {
                publicKey = rsa.ExportRSAPublicKey();
                privateKey = rsa.ExportRSAPrivateKey();
            }
        }

        public static RSA FromPrivateKey(byte[] key)
        {
            RSA rsa = RSA.Create();

            rsa.ImportRSAPrivateKey(new ReadOnlySpan<byte>(key), out var _);

            return rsa;
        }

        public static RSA FromPublicKey(byte[] key)
        {
            RSA rsa = RSA.Create();

            rsa.ImportRSAPublicKey(new ReadOnlySpan<byte>(key), out var _);

            return rsa;
        }
    }
}
