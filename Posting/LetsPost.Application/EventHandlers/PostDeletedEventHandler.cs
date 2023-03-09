using AutoMapper;
using LetsPost.Domain.Documents;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.EventHandlers;
public class PostDeletedEventHandler : INotificationHandler<PostDeletedEvent>
{
    private readonly IPostDocRepository _postDocRepository;
    public PostDeletedEventHandler(IPostDocRepository postDocRepository)
    {
        _postDocRepository = postDocRepository;
    }

    public async Task Handle(PostDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _postDocRepository.DeleteAsync(notification.Id);
    }

}
