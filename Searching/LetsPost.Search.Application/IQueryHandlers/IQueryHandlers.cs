using LetsPost.Search.Application.Queries;

namespace LetsPost.Search.Application.IQueryHandlers;
public interface IQueryHandlers<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellation);
}
