using LetsPost.Application;
using LetsPost.Application.Command;
using LetsPost.Application.DTO;
using LetsPost.Application.DTO.Requests;
using LetsPost.Application.IQueryHandlers;
using LetsPost.Application.Pipelines;
using LetsPost.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LetsPost.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly ICommandPipeline _commandPipeline;
    private readonly IQueryPipeline _queryPipeline;
    public PostController(ICommandPipeline commandPipeline, IQueryPipeline queryPipeline)
    {
        _commandPipeline = commandPipeline;
        _queryPipeline = queryPipeline;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostRequest post)
    {
        var command = new CreatePostCommand(post.Title, post.Content, post.Author);

        return await _commandPipeline.SendAsync<CreatePostCommand, PostDto>(command);

    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> Get()
    {
        var result = await _queryPipeline.SendAsync<GetPostsQuery, List<PostDto>>(new GetPostsQuery());

        if (result == null)
            return NotFound();

        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetById(int id)
    {
        var query = new GetPostQuery(id);
        var result = await _queryPipeline.SendAsync<GetPostQuery, PostDto>(query);

        if (result == null)
            return NotFound();

        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> Update(int id, UpdatePostRequest request)
    {
        if (id != request.Id)
            return BadRequest("Invalid Id");

        var command = new UpdatePostCommand(request.Id, request.Title, request.Content, request.Author);

        var result = await _commandPipeline.SendAsync<UpdatePostCommand, PostDto>(command);
        if (result == null)
            return BadRequest("Failed to update");

        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]int id)
    {
        var command = new DeletePostCommand(id);
        await _commandPipeline.SendAsync<DeletePostCommand, Unit>(command);
        return Ok();
    }
}
