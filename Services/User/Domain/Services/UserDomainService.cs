using UserService.Domain.Entities;
using Shared.Messaging;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Repositories;
using Shared.Events;

namespace UserService.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public UserDomainService(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task AddUserAsync(User user)
        {
            // Add the user to the database
            await _userRepository.AddAsync(user);

        }
        
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }
    }
}
