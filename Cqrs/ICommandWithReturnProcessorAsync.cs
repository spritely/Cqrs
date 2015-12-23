// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandWithReturnProcessor.cs">
//     Copyright (c) 2015. All rights reserved. Licensed under the MIT license. See LICENSE file in
//     the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    /// Finds the correct asychronous command handler and invokes it.
    /// </summary>
    public interface ICommandWithReturnProcessorAsync
    {
        /// <summary>
        /// Processes the specified command asychronously by finding the appropriate handler and
        /// invoking it.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="commandWithReturn">The command to process.</param>
        /// <returns>The command result.</returns>
        Task<TResult> ProcessAsync<TResult>(ICommandWithReturn<TResult> commandWithReturn);
    }
}
