namespace Spritely.Cqrs
{
    /// <summary>
    ///     Finds the correct query handler and invokes it.
    /// </summary>
    public interface IQueryProcessor
    {
        /// <summary>
        ///     Processes the specified query by finding the appropriate handler and invoking it.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to process.</param>
        /// <returns>The query result.</returns>
        TResult Process<TResult>(IQuery<TResult> query);
    }
}
