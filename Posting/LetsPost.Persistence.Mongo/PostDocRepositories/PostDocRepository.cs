using LetsPost.Domain;
using LetsPost.Domain.Documents;
using LetsPost.Persistence.IRepositories;
using MongoDB.Driver;

namespace LetsPost.Persistence.Mongo.PostDocRepositories;
public class PostDocRepository : IPostDocRepository
{
    private readonly IPostDocumentsContext _context;
    public PostDocRepository(IPostDocumentsContext context)
    {
        _context = context;
    }
    public async Task AddAsync(PostDocument post)
    {
        await _context.Posts.InsertOneAsync(post);
    }

    public async Task UpdateAsync(PostDocument post)
    {
        var filter = Builders<PostDocument>.Filter.Eq("PostId", post.PostId);
        var docs = _context.Posts.Find(filter);

        if (!await docs.AnyAsync())
        {
            await AddAsync(post);
            return;
        }
        await docs.ForEachAsync(doc =>
        {
            doc.Title = post.Title;
            doc.Content = post.Content;
            doc.Author = post.Author;
            doc.UpdatedAt = post.UpdatedAt;
            doc.CreatedAt = post.CreatedAt;
            _context.Posts.ReplaceOneAsync(filter, doc);
        });

    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<PostDocument>.Filter.Eq("PostId", id);

        await _context.Posts.DeleteOneAsync(filter);
    }
}
