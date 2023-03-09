using LetsPost.Application.Command;
using LetsPost.Application.DTO;

namespace LetsPost.Application;
public class CreatePostCommand : ICommand<PostDto>
{
    public CreatePostCommand(string title, string content, string author)
    {
        Title = title;
        Content = content;
        Author = author;
    }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
}
