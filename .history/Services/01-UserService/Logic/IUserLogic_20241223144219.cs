using UserService.Data.Entities;

namespace UserService.Logic
{
    public interface IUserLogic
    {
        Task RegisterUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
