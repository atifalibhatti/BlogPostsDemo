using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Domain;
public static class Module
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, string connectionString, string docConnectionString, string docDbName)
    {
        services.AddScoped<IPostDocumentsContext, PostDocumentsContext>(sp =>
        {
            return new PostDocumentsContext(docConnectionString, docDbName);
        });
        services.AddDbContext<PostsDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}
