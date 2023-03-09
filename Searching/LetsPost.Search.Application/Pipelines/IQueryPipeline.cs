using LetsPost.Search.Application.Queries;

namespace LetsPost.Search.Application.Pipelines;
public interface IQueryPipeline
{
    Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
