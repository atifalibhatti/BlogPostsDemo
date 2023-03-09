using LetsPost.Search.Domain;
using LetsPost.Search.Persistence.IRepositories;
using LetsPost.Search.Persistence.Mongo.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Search.Persistence.Mongo;
public static class Module
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services, string docConnectionString, string docDbName)
    {
        services.AddScoped<IPostDocumentsContext, PostDocumentsContext>(sp =>
        {
            return new PostDocumentsContext(docConnectionString, docDbName);
        });
        services.AddTransient<IPostDocRepository, PostDocRepository>();
        return services;
    }
}