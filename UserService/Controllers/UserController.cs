// controller for user service
// By Maitham Al-rubaye

using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        // {
        //     return Ok(await _userRepository.GetUsers());
        // }

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
        // [HttpPost("api/Register")]
        // public async Task<ActionResult<User>> AddUser(User user)
        // {
        //     await _userRepository.AddUser(user);

        //     return CreatedAtAction("GetUser", new { id = user.Id }, user);
        // }

        // PUT: User/Update/5
        // [HttpPut("Update/{id}")]
        // public async Task<ActionResult<User>> UpdateUser(int id, User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return BadRequest();
        //     }
        //     await _userRepository.UpdateUser(user);

        //     return NoContent();
        // }

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

        // GET: User
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetUsers();
            return View(users);
        }

        // GET: User/Register
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == null)
                {
                    ModelState.AddModelError(nameof(user.Password), "Password cannot be null");
                    return BadRequest(ModelState);
                }
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                await _userRepository.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateUser(updatedUser);
                }
                catch
                {
                    var user = await _userRepository.GetUser(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(updatedUser);
        }
    }
}