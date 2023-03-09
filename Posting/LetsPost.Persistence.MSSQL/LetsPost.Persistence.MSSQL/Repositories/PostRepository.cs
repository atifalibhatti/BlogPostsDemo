using LetsPost.Domain;
using LetsPost.Domain.Entities;
using LetsPost.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LetsPost.Persistence.MSSQL.Repositories;
public class PostRepository : IPostRepository
{
    PostsDbContext _context;
    public PostRepository(PostsDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Post entity)
    {
        await _context.Posts.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task UpdateByIdAsync(Post entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Post entity)
    {
        _context.Posts.Remove(entity);
        await _context.SaveChangesAsync();
    }

}
