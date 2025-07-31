namespace Auth.Application.Abstractions.Providers
{
    public interface IPasswordHashProvider
    {
        /// <summary>
        /// Hashes <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Hash(string value);
    }
}
