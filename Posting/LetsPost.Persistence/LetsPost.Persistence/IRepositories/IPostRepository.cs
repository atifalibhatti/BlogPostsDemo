using LetsPost.Domain.Entities;

namespace LetsPost.Persistence.IRepositories;
public interface IPostRepository
{
    Task AddAsync(Post entity);
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post> GetByIdAsync(int id);
    Task UpdateByIdAsync(Post entity);
    Task DeleteAsync(Post entity);
}
