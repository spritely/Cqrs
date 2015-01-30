namespace Spritely.Cqrs
{
    /// <summary>
    ///     Represents a command handler (typically a database write operation).
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand>
    {
        /// <summary>
        ///     Handles the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        void Handle(TCommand command);
    }
}
