using Shared.Persistence;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        // Additional methods specific to User can be added here if needed
    }
}
