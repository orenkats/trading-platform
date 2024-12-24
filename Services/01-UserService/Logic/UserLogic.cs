using UserService.Data.Entities;
using Shared.Messaging;
using UserService.Data.Repositories;
using Shared.Events;

namespace UserService.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public UserLogic(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task AddUserAsync(User user)
        {
            // Add the user to the database
            await _userRepository.AddAsync(user);

            // Publish UserCreatedEvent to RabbitMQ
            var userCreatedEvent = new UserCreatedEvent
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email, // Use actual property for email if exists
                CreatedAt = user.CreatedAt
            };

            _eventBus.Publish(userCreatedEvent, "UserExchange");
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
