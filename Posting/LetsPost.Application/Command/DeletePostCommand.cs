using LetsPost.Application.DTO;
using MediatR;

namespace LetsPost.Application.Command;
public class DeletePostCommand : ICommand<Unit>
{
    public DeletePostCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}