using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Search.Domain;
public static class Module
{
    public static IServiceCollection AddSearchDomainServices(this IServiceCollection services, string docConnectionString, string docDbName)
    {
        services.AddScoped<IPostDocumentsContext, PostDocumentsContext>(sp =>
        {
            return new PostDocumentsContext(docConnectionString, docDbName);
        });       
        return services;
    }
}
