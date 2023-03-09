using LetsPost.Domain.Documents;
using MongoDB.Driver;

namespace LetsPost.Domain
{
    public interface IPostDocumentsContext
    {
        IMongoCollection<PostDocument> Posts { get; }
    }
}