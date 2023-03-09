using LetsPost.Search.Application.DTO;
using LetsPost.Search.Persistence.IQueries;

namespace LetsPost.Search.Application.Queries;
public class SearchPostsQuery : IQuery<List<PostDto>>, ISearchPostsQuery
{
    public SearchPostsQuery(string searchterm)
    {
        Searchterm = searchterm;
    }
    public string Searchterm { get; set; }
    public List<PostDto> Result { get; set; }
}
