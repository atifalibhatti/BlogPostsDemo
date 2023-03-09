using LetsPost.Domain.Entities;
using MediatR;

namespace LetsPost.Domain.Events;
public class PostUpdatedEvent : IDomainEvent, INotification
{
    public Post Post { get; }
    public PostUpdatedEvent(Post post)
    {
        Post = post;
    }
}

