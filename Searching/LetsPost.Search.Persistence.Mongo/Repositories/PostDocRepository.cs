using LetsPost.Search.Domain;
using LetsPost.Search.Domain.Documents;
using LetsPost.Search.Persistence.IQueries;
using LetsPost.Search.Persistence.IRepositories;
using MongoDB.Driver;

namespace LetsPost.Search.Persistence.Mongo.Repositories;
public class PostDocRepository : IPostDocRepository
{
    private readonly IPostDocumentsContext _context;
    public PostDocRepository(IPostDocumentsContext context)
    {
        _context = context;
    }

    public async Task<List<PostDocument>> Search(ISearchPostsQuery query)
    {
        var options = new TextSearchOptions
        {
            CaseSensitive = false,
            DiacriticSensitive = false,
        };

        FilterDefinition<PostDocument> filter = null;
        if (!string.IsNullOrEmpty(query.Searchterm))
            filter = Builders<PostDocument>.Filter.Text(query.Searchterm, options);

        //if (!string.IsNullOrEmpty(query.Content))
        //    filter = filter == null ? Builders<PostDocument>.Filter.Text(query.Content, options) : filter | Builders<PostDocument>.Filter.Text(query.Content, options);

        //if (!string.IsNullOrEmpty(query.Author))
        //    filter = filter == null ? Builders<PostDocument>.Filter.Text(query.Author, options) : filter | Builders<PostDocument>.Filter.Text(query.Author, options);

        if (filter == null)
            return new List<PostDocument>();

        return await _context.Posts.Find(filter).ToListAsync();

    }
}
