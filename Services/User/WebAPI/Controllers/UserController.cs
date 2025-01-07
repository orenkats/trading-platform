using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Queries;
using UserService.Domain.Entities;

namespace UserService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly GetUserByIdQuery _getUserByIdQuery;
        private readonly GetAllUsersQuery _getAllUsersQuery;
        private readonly AddUserCommand _addUserCommand;
        private readonly DeleteUserCommand _deleteUserCommand;

        public UserController(
            GetUserByIdQuery getUserByIdQuery,
            GetAllUsersQuery getAllUsersQuery,
            AddUserCommand addUserCommand,
            DeleteUserCommand deleteUserCommand)
        {
            _getUserByIdQuery = getUserByIdQuery;
            _getAllUsersQuery = getAllUsersQuery;
            _addUserCommand = addUserCommand;
            _deleteUserCommand = deleteUserCommand;
        }

        [HttpGet]
        [Route("get-user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _getUserByIdQuery.ExecuteAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _getAllUsersQuery.ExecuteAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _addUserCommand.ExecuteAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _deleteUserCommand.ExecuteAsync(id);
            return NoContent();
        }
    }
}
