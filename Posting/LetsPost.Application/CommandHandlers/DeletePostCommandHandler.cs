using AutoMapper;
using LetsPost.Application.Command;
using LetsPost.Application.DTO;
using LetsPost.Application.ICommandHandlers;
using LetsPost.Domain.Entities;
using LetsPost.Domain.Events;
using LetsPost.Persistence.IRepositories;
using MediatR;

namespace LetsPost.Application.CommandHandlers;
public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Unit>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DeletePostCommandHandler(IPostRepository postRepository, IMapper mapper, IMediator mediator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<Unit> HandleAsync(DeletePostCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postRepository.GetByIdAsync(command.Id);

            if (post == null)
            {
                throw new ArgumentException("Post not found", nameof(command.Id));
            }
            await _postRepository.DeleteAsync(post);


            //Raise the PostDeletedEvent
            PostDeletedEvent postDeletedEvent = new(command.Id);
            await _mediator.Publish(postDeletedEvent, cancellationToken);

            return Unit.Value;
        }
        catch (Exception)
        {
            //Log exceptions here...
            throw;
        }
    }
}
