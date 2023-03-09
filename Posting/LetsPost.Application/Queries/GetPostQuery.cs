using LetsPost.Application.DTO;

namespace LetsPost.Application.Queries;
public class GetPostQuery : IQuery<PostDto>
{
    public GetPostQuery(int id)
    {
        Id = id;
    }
    public int Id { get; }
    public PostDto Result { get; set; }
}
