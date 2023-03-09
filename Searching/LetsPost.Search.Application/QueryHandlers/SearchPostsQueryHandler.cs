using AutoMapper;
using LetsPost.Search.Application.DTO;
using LetsPost.Search.Application.IQueryHandlers;
using LetsPost.Search.Application.Queries;
using LetsPost.Search.Persistence.IRepositories;

namespace LetsPost.Search.Application.QueryHandlers;
public class SearchPostsQueryHandler : IQueryHandlers<SearchPostsQuery, List<PostDto>>
{
    private readonly IPostDocRepository _postDocRepository;
    private readonly IMapper _mapper;
    public SearchPostsQueryHandler(IPostDocRepository postDocRepository, IMapper mapper)
    {
        _postDocRepository = postDocRepository;
        _mapper = mapper;
    }

    public async Task<List<PostDto>> HandleAsync(SearchPostsQuery query, CancellationToken cancellation)
    {
        var posts = await _postDocRepository.Search(query);
        return _mapper.Map<List<PostDto>>(posts);
    }
}

