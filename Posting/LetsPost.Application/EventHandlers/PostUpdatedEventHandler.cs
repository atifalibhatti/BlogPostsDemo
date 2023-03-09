using AutoMapper;
using LetsPost.Domain.Documents;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.EventHandlers;
public class PostUpdatedEventHandler : INotificationHandler<PostUpdatedEvent>
{
    private readonly IPostDocRepository _postDocRepository;
    private readonly IMapper _mapper;
    public PostUpdatedEventHandler(IPostDocRepository postDocRepository, IMapper mapper)
    {
        _postDocRepository = postDocRepository;
        _mapper = mapper;
    }

    public async Task Handle(PostUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var doc = _mapper.Map<PostDocument>(notification.Post);
        await _postDocRepository.UpdateAsync(doc);
    }

}
