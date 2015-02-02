namespace Spritely.Cqrs
{
    // TODO: Write unit tests for CommandWithReturnProcessor
    /// <inheritdoc />
    public sealed class CommandWithReturnProcessor : ICommandWithReturnProcessor
    {
        private readonly GetInstance getInstance;

        /// <inheritdoc />
        public CommandWithReturnProcessor(GetInstance getInstance)
        {
            this.getInstance = getInstance;
        }

        /// <inheritdoc />
        public TResult Process<TResult>(ICommandWithReturn<TResult> commandWithReturn)
        {
            var handlerType = typeof(ICommandWithReturnHandler<,>).MakeGenericType(commandWithReturn.GetType(), typeof(TResult));

            dynamic handler = this.getInstance(handlerType);

            return handler.Handle((dynamic)commandWithReturn);
        }
    }
}
