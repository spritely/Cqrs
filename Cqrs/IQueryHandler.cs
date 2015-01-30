namespace Spritely.Cqrs
{
    /// <summary>
    ///     Represents a query handler (typically a database read operation).
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        ///     Handles the specified query.
        /// </summary>
        /// <param name="query">The query to handle.</param>
        /// <returns>The query results.</returns>
        TResult Handle(TQuery query);
    }
}
