using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Queries
{
    public class GetUserByIdQuery
    {
        private readonly IUserDomainService _userDomainService;

        public GetUserByIdQuery(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task<User?> ExecuteAsync(Guid userId)
        {
            return await _userDomainService.GetUserByIdAsync(userId);
        }
    }
}
