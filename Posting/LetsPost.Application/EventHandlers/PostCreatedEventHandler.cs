using AutoMapper;
using LetsPost.Domain.Documents;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.EventHandlers;
public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
{
    private readonly IPostDocRepository _postDocRepository;
    private readonly IMapper _mapper;
    public PostCreatedEventHandler(IPostDocRepository postDocRepository, IMapper mapper)
    {
        _postDocRepository = postDocRepository;
        _mapper = mapper;
    }

    public async Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        var doc = _mapper.Map<PostDocument>(notification.Post);
        await _postDocRepository.AddAsync(doc);
    }

}
