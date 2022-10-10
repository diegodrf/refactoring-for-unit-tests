using Store.Domain.Commands.Interfaces;


namespace Store.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : Icommand
    {
        ICommandResult Handle(T command);
    }
}
