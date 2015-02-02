namespace Spritely.Cqrs
{
    // TODO: Write unit tests for CommandProcessor
    /// <inheritdoc />
    public sealed class CommandProcessor : ICommandProcessor
    {
        private readonly GetInstance getInstance;

        /// <inheritdoc />
        public CommandProcessor(GetInstance getInstance)
        {
            this.getInstance = getInstance;
        }

        /// <inheritdoc />
        public void Process(ICommand command)
        {
            var handlerType = typeof (ICommandHandler<>).MakeGenericType(command.GetType());

            dynamic handler = this.getInstance(handlerType);

            handler.Handle((dynamic) command);
        }
    }
}
