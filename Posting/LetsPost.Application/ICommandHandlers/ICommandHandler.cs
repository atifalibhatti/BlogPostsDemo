using LetsPost.Application.Command;

namespace LetsPost.Application.ICommandHandlers;
public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
