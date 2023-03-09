using AutoMapper;
using LetsPost.Application.Command;
using LetsPost.Application.DTO;
using LetsPost.Application.ICommandHandlers;
using LetsPost.Domain.Entities;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.CommandHandlers;
public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper, IMediator mediator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<PostDto> HandleAsync(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postRepository.GetByIdAsync(command.Id);

            if (post == null)
            {
                throw new ArgumentException("Post not found", nameof(command.Id));
            }

            post = _mapper.Map<UpdatePostCommand, Post>(command, post);

            await _postRepository.UpdateByIdAsync(post);

            //Raise the PostUpdatedEvent
            PostUpdatedEvent postUpdatedEvent = new(post);
            await _mediator.Publish(postUpdatedEvent, cancellationToken);

            return _mapper.Map<PostDto>(post);
        }
        catch (Exception)
        {
            //Log exceptions here...
            throw;
        }
    }
}
