namespace Spritely.Cqrs
{
    using System;

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
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            dynamic handler = this.getInstance(handlerType);

            handler.Handle((dynamic)command);
        }

        /// <inheritdoc />
        public void ProcessAsync(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            dynamic handler = this.getInstance(handlerType);

            handler.Handle((dynamic)command);
        }
    }
}
