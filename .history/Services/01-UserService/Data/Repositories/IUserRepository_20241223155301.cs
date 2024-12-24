using Shared.Persistence;
using UserService.Data.Entities;

namespace UserService.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        // Additional methods specific to User can be added here if needed
    }
}
