namespace Spritely.Cqrs
{
    /// <summary>
    ///     Finds the correct command handler and invokes it.
    /// </summary>
    public interface ICommandProcessor
    {
        /// <summary>
        ///     Processes the specified command by finding the appropriate handler and invoking it.
        /// </summary>
        /// <param name="command">The command to process.</param>
        /// <returns>The command result.</returns>
        void Process(ICommand command);
    }
}
