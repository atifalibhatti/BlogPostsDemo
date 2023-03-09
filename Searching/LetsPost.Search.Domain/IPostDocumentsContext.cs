using LetsPost.Search.Domain.Documents;
using MongoDB.Driver;

namespace LetsPost.Search.Domain
{
    public interface IPostDocumentsContext
    {
        IMongoCollection<PostDocument> Posts { get; }
    }
}