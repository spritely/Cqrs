namespace Spritely.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Finds the correct asynchronous command handler and invokes it.
    /// </summary>
    public interface ICommandProcessorAsync
    {
        /// <summary>
        ///     Processes the specified command asychronously by finding the appropriate handler and invoking it.
        /// </summary>
        /// <param name="command">The command to process.</param>
        /// <returns>The command result.</returns>
        Task ProcessAsync(ICommand command);
    }
}
