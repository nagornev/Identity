namespace Auth.Application.Abstractions.Providers
{
    public interface IPasswordHashProvider
    {
        /// <summary>
        /// Hashes <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string Hash(string value, string salt);
    }
}
