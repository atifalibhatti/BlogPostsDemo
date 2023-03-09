using LetsPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LetsPost.Domain;
public class PostsDbContext : DbContext
{
    public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options)
    {

    }
    public DbSet<Post> Posts { get; set; }
}