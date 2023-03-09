using AutoMapper;
using LetsPost.Application.DTO;
using LetsPost.Application.ICommandHandlers;
using LetsPost.Domain.Entities;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.CommandHandlers;
public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper, IMediator mediator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<PostDto> HandleAsync(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(command);
        await _postRepository.AddAsync(post);

        //Raise the PostCreatedEvent
        PostCreatedEvent postCreatedEvent = new(post);
        await _mediator.Publish(postCreatedEvent, cancellationToken);

        return _mapper.Map<PostDto>(post);
    }
}
