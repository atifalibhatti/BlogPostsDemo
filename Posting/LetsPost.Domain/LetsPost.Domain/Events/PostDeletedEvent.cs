using LetsPost.Domain.Entities;
using MediatR;

namespace LetsPost.Domain.Events;
public class PostDeletedEvent : IDomainEvent, INotification
{
    public int Id { get; }
    public PostDeletedEvent(int id)
    {
        Id = id;
    }
}

