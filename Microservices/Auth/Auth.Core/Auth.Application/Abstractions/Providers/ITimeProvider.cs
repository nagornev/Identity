namespace Auth.Application.Abstractions.Providers
{
    public interface ITimeProvider
    {
        /// <summary>
        /// Returns the UTC time in seconds.
        /// </summary>
        /// <returns></returns>
        long NowUnix();

        /// <summary>
        /// Returns the UTC time in seconds.
        /// </summary>
        /// <returns></returns>
        DateTime NowDateTime();
    }
}
