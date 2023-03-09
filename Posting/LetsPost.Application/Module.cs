using LetsPost.Application.Command;
using LetsPost.Application.CommandHandlers;
using LetsPost.Application.DTO;
using LetsPost.Application.EventHandlers;
using LetsPost.Application.ICommandHandlers;
using LetsPost.Application.IQueryHandlers;
using LetsPost.Application.Pipelines;
using LetsPost.Application.Queries;
using LetsPost.Application.QueryHandlers;
using LetsPost.Domain;
using LetsPost.Domain.Events;
using LetsPost.Persistence.Mongo;
using LetsPost.Persistence.MSSQL;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPost.Application;
public static class Module
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, string connectionString, string docConnectionString, string docDbName)
    {
        services.AddDomainServices(connectionString, docConnectionString, docDbName);
        services.AddMongoServices();
        services.AddSqlServices();
        services.AddAutoMapper(typeof(Module).Assembly);
        return services.AddServices();
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[] { typeof(Module).Assembly }));

        services.AddScoped<ICommandPipeline, CommandPipeline>();
        services.AddScoped<IQueryPipeline, QueryPipeline>();

        services.AddTransient<ICommandHandler<CreatePostCommand, PostDto>, CreatePostCommandHandler>();
        services.AddTransient<ICommandHandler<UpdatePostCommand, PostDto>, UpdatePostCommandHandler>();
        services.AddTransient<ICommandHandler<DeletePostCommand, Unit>, DeletePostCommandHandler>();

        services.AddTransient<IQueryHandlers<GetPostQuery, PostDto>, GetPostQueryHandler>();
        services.AddTransient<IQueryHandlers<GetPostsQuery, List<PostDto>>, GetPostsQueryHandler>();

        services.AddTransient<INotificationHandler<PostCreatedEvent>, PostCreatedEventHandler>();
        services.AddTransient<INotificationHandler<PostUpdatedEvent>, PostUpdatedEventHandler>();
        services.AddTransient<INotificationHandler<PostDeletedEvent>, PostDeletedEventHandler>();

        return services;

    }
}
