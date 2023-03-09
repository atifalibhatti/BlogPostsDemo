using LetsPost.Persistence.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using LetsPost.Persistence.Mongo.PostDocRepositories;

namespace LetsPost.Persistence.Mongo;
public static class Module
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services)
    {
        services.AddTransient<IPostDocRepository, PostDocRepository>();
        return services;
    }
}
