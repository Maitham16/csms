// Controller for user service
// By Maitham Al-rubaye

using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;
using UserService.RabbitMQ;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly MessageQueueService _messageQueueService;

        public UserController(UserRepository userRepository, ILogger<UserController> logger, MessageQueueService messageQueueService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _messageQueueService = messageQueueService;
        }

        // POST: /user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user, string password)
        {
            try
            {
                _logger.LogInformation("Registering user");
                var result = await _userRepository.RegisterUser(user, password);
                _logger.LogInformation("User registered");
                // send message to rabbitmq
                _messageQueueService.SendMessage(new { Type = "UserRegistered", UserId = result.Id });
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registration failed: {ex.Message}\n{ex.StackTrace}");
                return BadRequest("Registration failed.");
            }
        }

        // POST: /user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            var token = await _userRepository.LoginUser(email, password);
            if (token != null)
            {
                _logger.LogInformation("token: " + token);
                return Ok(new { Token = token });
            }
            _logger.LogInformation("Unauthorized login attempt.");
            return Unauthorized();
        }

        // GET: /user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                _logger.LogInformation("Getting user");
                var user = await _userRepository.GetUser(id);
                if (user == null)
                {
                    _logger.LogError("Failed to get user");
                    return NotFound("User not found.");
                }
                _logger.LogInformation("User retrieved");
                return Ok(user);
            }
            catch
            {
                _logger.LogError("Failed to get user");
                return BadRequest("Failed to get user.");
            }
        }

        // GET: /user
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation("Getting users");
                var users = await _userRepository.GetUsers();
                _logger.LogInformation("Users retrieved");
                return Ok(users);
            }
            catch
            {
                _logger.LogError("Failed to get users");
                return BadRequest("Failed to get users.");
            }
        }

        // PUT: /user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation("Updating user");
                var updatedUser = await _userRepository.UpdateUser(user);
                _logger.LogInformation("User updated");
                return Ok(updatedUser);
            }
            catch
            {
                _logger.LogError("Failed to update user");
                return BadRequest("Failed to update user.");
            }
        }

        // DELETE: /user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                _logger.LogInformation("Deleting user");
                var deletedUser = await _userRepository.DeleteUser(id);

                if (deletedUser == null)
                {
                    _logger.LogError("Failed to delete user");
                    return NotFound("User not found.");
                }
                _logger.LogInformation("User deleted");
                return Ok(deletedUser);
            }
            catch
            {
                _logger.LogError("Failed to delete user");
                return BadRequest("Failed to delete user.");
            }
        }

        // logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                _logger.LogInformation("Logging out user");
                await _userRepository.LogoutUser();
                _logger.LogInformation("User logged out");
                return Ok();
            }
            catch
            {
                _logger.LogError("Failed to logout user");
                return BadRequest("Failed to logout user.");
            }
        }

    }
}