// controller for user service
// By Maitham Al-rubaye

using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsers());
        }

        // GET: User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: User/Register
        [HttpPost("Register")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            await _userRepository.AddUser(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT: User/Update/5
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUser(user);

            return NoContent();
        }

        // DELETE: User/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _userRepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    }
}