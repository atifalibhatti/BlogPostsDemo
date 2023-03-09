using LetsPost.Application.DTO;

namespace LetsPost.Application.Queries;
public class GetPostsQuery : IQuery<List<PostDto>>
{
    public List<PostDto> Result { get; set; }
}
