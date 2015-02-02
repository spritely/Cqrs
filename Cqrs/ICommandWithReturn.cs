namespace Spritely.Cqrs
{
    /// <summary>
    ///     A command operation that returns a result. Consider using ICommand and IQuery to ICommandWithReturn instead.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class ICommandWithReturn<TResult>
    {
    }
}
