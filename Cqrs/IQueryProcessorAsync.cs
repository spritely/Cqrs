namespace Spritely.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    /// Finds the correct asynchronous query handler and invokes it.
    /// </summary>
    public interface IQueryProcessorAsync
    {
        /// <summary>
        /// Processes the specified query asynchronously by finding the appropriate handler and
        /// invoking it.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to process.</param>
        /// <returns>The query result.</returns>
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}
