using LetsPost.Search.Application.DTO;
using LetsPost.Search.Application.IQueryHandlers;
using LetsPost.Search.Application.Pipelines;
using LetsPost.Search.Application.Queries;
using LetsPost.Search.Application.QueryHandlers;
using LetsPost.Search.Domain;
using LetsPost.Search.Persistence.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Search.Application;
public static class Module
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, string docDbConnection, string docDbName)
    {
        services.AddSearchDomainServices(docDbConnection, docDbName);
        services.AddMongoServices(docDbConnection, docDbName);
        services.AddAutoMapper(typeof(Module).Assembly);
        return services.AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[] { typeof(Module).Assembly }));

        services.AddScoped<IQueryPipeline, QueryPipeline>();
        services.AddTransient<IQueryHandlers<SearchPostsQuery, List<PostDto>>, SearchPostsQueryHandler>();
        return services;
    }
}
