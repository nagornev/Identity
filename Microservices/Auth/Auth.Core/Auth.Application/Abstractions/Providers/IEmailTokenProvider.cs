namespace Auth.Application.Abstractions.Providers
{
    public interface IEmailTokenProvider
    {
        /// <summary>
        /// Creates email token for <paramref name="user"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> CreateAsync(Guid userId, string emailAddress, CancellationToken cancellation = default);
    }
}
