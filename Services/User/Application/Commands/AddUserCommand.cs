using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Commands
{
    public class AddUserCommand
    {
        private readonly IUserDomainService _userDomainService;

        public AddUserCommand(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task ExecuteAsync(User user)
        {
            await _userDomainService.AddUserAsync(user);
        }
    }
}
