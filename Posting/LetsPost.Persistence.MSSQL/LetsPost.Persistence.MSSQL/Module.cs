using LetsPost.Persistence.IRepositories;
using LetsPost.Persistence.MSSQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Persistence.MSSQL;
public static class Module
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        return services;
    }
}