namespace LetsPost.Search.Application.Queries;
public interface IQuery<TResult>
{
    public TResult Result { get; set; }
}
