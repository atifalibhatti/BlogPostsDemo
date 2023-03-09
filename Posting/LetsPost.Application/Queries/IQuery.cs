namespace LetsPost.Application.Queries;
public interface IQuery<TResult>
{
    TResult Result { get; set; }
}
