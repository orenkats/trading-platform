using Shared.Persistence;
using UserService.Data.Entities;

namespace UserService.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }
    }
}
