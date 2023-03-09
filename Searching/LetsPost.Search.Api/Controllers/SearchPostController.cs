using LetsPost.Search.Application.DTO;
using LetsPost.Search.Application.DTO.Request;
using LetsPost.Search.Application.Pipelines;
using LetsPost.Search.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LetsPost.Search.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchPostController : ControllerBase
{
    private readonly IQueryPipeline _queryPipeline;
    public SearchPostController(IQueryPipeline queryPipeline)
    {
        _queryPipeline = queryPipeline;
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> Get([FromQuery] SearchPostRequest request)
    {
        var query = new SearchPostsQuery(request.SearchTerm);
        var result = await _queryPipeline.SendAsync<SearchPostsQuery, List<PostDto>>(query);

        if (result == null)
            return NotFound();

        return result;
    }

}