using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Queries
{
    public class GetAllUsersQuery
    {
        private readonly IUserDomainService _userDomainService;

        public GetAllUsersQuery(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task<IEnumerable<User>> ExecuteAsync()
        {
            return await _userDomainService.GetAllUsersAsync();
        }
    }
}
