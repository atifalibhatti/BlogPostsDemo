
namespace LetsPost.UserManagement.Helpers
{
    public interface ITokenValidator
    {
        Task<bool> ValidateToken(string token);
    }
}