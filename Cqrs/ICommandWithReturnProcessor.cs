namespace Spritely.Cqrs
{
    /// <summary>
    ///     Finds the correct command handler and invokes it.
    /// </summary>
    public interface ICommandWithReturnProcessor
    {
        /// <summary>
        ///     Processes the specified command by finding the appropriate handler and invoking it.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command to process.</param>
        /// <returns>The command result.</returns>
        TResult Process<TResult>(ICommandWithReturn<TResult> command);
    }
}
