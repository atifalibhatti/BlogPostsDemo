using LetsPost.Application.IQueryHandlers;
using LetsPost.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Application.Pipelines;
public class QueryPipeline : IQueryPipeline
{
    private readonly IServiceProvider _serviceProvider;
    public QueryPipeline(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetService<IQueryHandlers<TQuery, TResult>>();
        if (handler == null)
            throw new InvalidOperationException($"No Query Handler found for ${typeof(TQuery).FullName}");

        try
        {
            return await handler.HandleAsync(query, CancellationToken.None);
        }
        catch (Exception)
        {
            //log exceptions here
            throw;
        }
    }
}