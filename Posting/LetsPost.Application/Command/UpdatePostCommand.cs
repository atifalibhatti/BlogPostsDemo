using LetsPost.Application.DTO;

namespace LetsPost.Application.Command;
public class UpdatePostCommand : ICommand<PostDto>
{
    public UpdatePostCommand(int id, string title, string content, string author)
    {
        Id = id;
        Title = title;
        Content = content;
        Author = author;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
}