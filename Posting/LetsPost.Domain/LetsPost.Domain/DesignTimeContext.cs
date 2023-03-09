using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LetsPost.Domain;
public class DesignTimeContext : IDesignTimeDbContextFactory<PostsDbContext>
{
    public PostsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostsDbContext>();
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-P246133\SQL;Database=PostDemoDb;User Id=sa;Password=123456;");
        return new PostsDbContext(optionsBuilder.Options);
    }
}
