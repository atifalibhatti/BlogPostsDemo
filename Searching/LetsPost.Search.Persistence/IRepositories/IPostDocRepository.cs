using LetsPost.Search.Domain.Documents;
using LetsPost.Search.Persistence.IQueries;

namespace LetsPost.Search.Persistence.IRepositories;
public interface IPostDocRepository
{
    Task<List<PostDocument>> Search(ISearchPostsQuery query);
}
