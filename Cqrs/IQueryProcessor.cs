namespace Spritely.Cqrs
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}
