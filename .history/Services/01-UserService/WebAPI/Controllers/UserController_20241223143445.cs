using Microsoft.AspNetCore.Mvc;
using UserService.Data.Entities;
using UserService.Data.Repositories;
using Shared.RabbitMQ;
using Shared.Messaging;

namespace UserService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public UserController(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);

                // Publish event
                var userRegisteredEvent = new UserRegisteredEvent
                {
                    UserId = user.Id,
                    UserName = user.Name
                };
                _eventBus.Publish(userRegisteredEvent, "UserExchange");

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }
    }
}
