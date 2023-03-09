using LetsPost.Application.Queries;

namespace LetsPost.Application.IQueryHandlers;
public interface IQueryHandlers<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellation);
}