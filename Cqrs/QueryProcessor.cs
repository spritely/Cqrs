namespace Spritely.Cqrs
{

    // TODO: Write unit tests for QueryProcessor
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly GetInstance getInstance;

        public QueryProcessor(GetInstance getInstance)
        {
            this.getInstance = getInstance;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = this.getInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}
