namespace Spritely.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Represents an asynchronous command handler (typically a database write operation).
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        ///     Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        Task HandleAsync(TCommand command);
    }
}
