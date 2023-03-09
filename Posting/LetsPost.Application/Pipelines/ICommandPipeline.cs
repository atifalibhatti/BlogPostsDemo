using LetsPost.Application.Command;

namespace LetsPost.Application.Pipelines
{
    public interface ICommandPipeline
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    }
}