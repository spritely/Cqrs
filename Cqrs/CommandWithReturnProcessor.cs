// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandWithReturnProcessor.cs">
//     Copyright (c) 2015. All rights reserved. Licensed under the MIT license. See LICENSE file in
//     the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs
{
    using System;
    using System.Threading.Tasks;

    // TODO: Write unit tests for CommandWithReturnProcessor
    /// <inheritdoc />
    public sealed class CommandWithReturnProcessor : ICommandWithReturnProcessor, ICommandWithReturnProcessorAsync
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
            if (commandWithReturn == null)
            {
                throw new ArgumentNullException("commandWithReturn");
            }

            var handlerType = typeof(ICommandWithReturnHandler<,>).MakeGenericType(commandWithReturn.GetType(), typeof(TResult));

            dynamic handler = this.getInstance(handlerType);

            return handler.Handle((dynamic)commandWithReturn);
        }

        /// <inheritdoc />
        public async Task<TResult> ProcessAsync<TResult>(ICommandWithReturn<TResult> commandWithReturn)
        {
            if (commandWithReturn == null)
            {
                throw new ArgumentNullException("commandWithReturn");
            }

            var handlerType = typeof(ICommandWithReturnHandlerAsync<,>).MakeGenericType(commandWithReturn.GetType(), typeof(TResult));

            dynamic handler = this.getInstance(handlerType);

            return await handler.HandleAsync((dynamic)commandWithReturn);
        }
    }
}
