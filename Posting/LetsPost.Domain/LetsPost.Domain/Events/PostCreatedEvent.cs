using LetsPost.Domain.Entities;
using MediatR;

namespace LetsPost.Domain.Events;
public class PostCreatedEvent : IDomainEvent, INotification
{
    public Post Post { get; }
    public PostCreatedEvent(Post post)
    {
        Post = post;
    }
}

