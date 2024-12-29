using UserService.Data.Entities;

namespace UserService.Logic
{
    public interface IUserLogic
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
    }
}
