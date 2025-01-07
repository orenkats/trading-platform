using Shared.Events;
using Shared.Messaging;
using UserService.Application.Commands;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly AddUserCommand _addUserCommand;

        public UserCreatedEventHandler(AddUserCommand addUserCommand)
        {
            _addUserCommand = addUserCommand;
        }

        public async Task HandleAsync(UserCreatedEvent userCreatedEvent)
        {
            // Create a new User entity from the event data
            var user = new User
            {
                Id = userCreatedEvent.UserId,
                Name = userCreatedEvent.Name,
                Email = userCreatedEvent.Email,
                Timestamp = userCreatedEvent.Timestamp
            };

            // Delegate the operation to the AddUserCommand
            await _addUserCommand.ExecuteAsync(user);
        }
    }
}
