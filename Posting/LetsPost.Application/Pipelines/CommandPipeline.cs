using LetsPost.Application.Command;
using LetsPost.Application.ICommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Application.Pipelines;
public class CommandPipeline : ICommandPipeline
{
    private readonly IServiceProvider _serviceProvider;
    public CommandPipeline(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
        if (handler == null)
            throw new InvalidOperationException($"No command handler found for {typeof(TCommand).FullName}");

        try
        {
            return await handler.HandleAsync(command, CancellationToken.None);
        }
        catch (Exception)
        {
            //log exceptions here
            throw;
        }

    }
}