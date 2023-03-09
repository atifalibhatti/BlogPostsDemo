using LetsPost.Application.Queries;

namespace LetsPost.Application.Pipelines;
public interface IQueryPipeline
{
    Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
