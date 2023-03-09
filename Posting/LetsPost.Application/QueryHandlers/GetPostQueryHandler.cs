using AutoMapper;
using LetsPost.Application.DTO;
using LetsPost.Application.IQueryHandlers;
using LetsPost.Application.Queries;
using LetsPost.Persistence.IRepositories;

namespace LetsPost.Application.QueryHandlers;
public class GetPostQueryHandler : IQueryHandlers<GetPostQuery, PostDto>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    public GetPostQueryHandler(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }
    public async Task<PostDto> HandleAsync(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(query.Id);
        return _mapper.Map<PostDto>(post);
    }
}