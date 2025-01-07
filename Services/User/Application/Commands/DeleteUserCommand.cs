using UserService.Domain.Interfaces;

namespace UserService.Application.Commands
{
    public class DeleteUserCommand
    {
        private readonly IUserDomainService _userDomainService;

        public DeleteUserCommand(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task ExecuteAsync(Guid userId)
        {
            await _userDomainService.DeleteUserAsync(userId);
        }
    }
}
