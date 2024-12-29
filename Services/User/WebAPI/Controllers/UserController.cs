using Microsoft.AspNetCore.Mvc;
using UserService.Data.Entities;
using UserService.Logic;

namespace UserService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        [Route("get-user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userLogic.GetUserByIdAsync(id);
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
            var users = await _userLogic.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userLogic.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut]
        [Route("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userLogic.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userLogic.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
