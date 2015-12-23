namespace Spritely.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an asynchronous command with return handler (a write operation that returns a value).
    /// </summary>
    /// <typeparam name="TCommandWithReturn">The type of the command parameters.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface ICommandWithReturnHandlerAsync<in TCommandWithReturn, TResult>
        where TCommandWithReturn : ICommandWithReturn<TResult>
    {
        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <returns>The command results.</returns>
        Task<TResult> HandleAsync(TCommandWithReturn command);
    }
}
