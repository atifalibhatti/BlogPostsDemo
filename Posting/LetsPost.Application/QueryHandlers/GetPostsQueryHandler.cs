using AutoMapper;
using LetsPost.Application.DTO;
using LetsPost.Application.IQueryHandlers;
using LetsPost.Application.Queries;
using LetsPost.Persistence.IRepositories;

namespace LetsPost.Application.QueryHandlers;
public class GetPostsQueryHandler : IQueryHandlers<GetPostsQuery, List<PostDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    public GetPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }
    public async Task<List<PostDto>> HandleAsync(GetPostsQuery query, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetAllAsync();
        return _mapper.Map<List<PostDto>>(posts);
    }
}