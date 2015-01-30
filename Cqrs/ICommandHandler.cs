namespace Spritely.Cqrs
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
