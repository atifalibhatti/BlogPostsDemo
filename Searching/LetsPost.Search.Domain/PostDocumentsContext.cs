using LetsPost.Search.Domain.Documents;
using MongoDB.Driver;

namespace LetsPost.Search.Domain;
public class PostDocumentsContext : IPostDocumentsContext
{
    private readonly IMongoDatabase _database;
    public PostDocumentsContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    public IMongoCollection<PostDocument> Posts => _database.GetCollection<PostDocument>("posts");
}
