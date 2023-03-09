using LetsPost.Domain.Documents;

namespace LetsPost.Persistence.IRepositories;
public interface IPostDocRepository
{
    Task AddAsync(PostDocument post);
    Task UpdateAsync(PostDocument post);
    Task DeleteAsync(int id);
}
