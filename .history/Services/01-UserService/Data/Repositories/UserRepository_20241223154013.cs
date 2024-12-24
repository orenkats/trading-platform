using Microsoft.EntityFrameworkCore;
using Shared.Persistence;
using Shared.MySQL;
using UserService.Data.Entities;

namespace UserService.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Additional methods specific to User can be implemented here
    }
}
