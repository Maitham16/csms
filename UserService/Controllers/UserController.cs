// Controller for user service
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

public class UserController : Controller
{
    private readonly IUserServices _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserServices userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        _logger.LogInformation("Register method called");
        if (ModelState.IsValid)
        {
            var user = await _userService.RegisterUser(
                model.Username!,
                model.FirstName!,
                model.LastName!,
                model.Birthdate!,
                model.Email!,
                model.Password!,
                model.PhoneNumber!,
                model.Address!,
                model.City!,
                model.ZipCode!,
                model.Country!,
                model.Role!);

            if (user != null)
            {
                _logger.LogInformation("Model received: {Model}", model);
                return Ok(new { Message = "Registration successful.", User = user });
            }

            _logger.LogInformation("User returned null for email: {Email}", model.Email);
            return BadRequest(new { Message = "Invalid registration attempt. User returned null for email." });  // return bad request status
        }

        _logger.LogWarning("Model state is invalid. Errors: {ModelStateErrors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        return BadRequest(new { Message = "Invalid registration attempt. Model state is invalid." });  // return bad request status
    }


    [AllowAnonymous]
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Attempting to log in user with email: {Email}", model.Email);
                var user = await _userService.LoginUser(model.Email!, model.Password!);

                if (user != null)
                {
                    _logger.LogInformation("Login attempt successful for user with email: {Email}", model.Email);
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogInformation("User returned null for email: {Email}", model.Email);
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid. Errors: {ModelStateErrors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
        }
        _logger.LogInformation("Login attempt failed for user with email: {Email}", model.Email);
        return View(model);
    }

    [HttpGet("User")]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetUsers();
        return View(users);
    }

    [HttpGet("User/Edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }

        var updateModel = new UpdateModel
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Birthdate = user.Birthdate,
            Email = user.Email,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            City = user.City,
            ZipCode = user.ZipCode,
            Country = user.Country,
            Role = user.Role
        };

        return View(updateModel); // make sure you pass UpdateModel not User
    }

    [HttpPost("User/Edit/{id}")]
    public async Task<IActionResult> Edit(string id, UpdateModel updateModel)
    {
        if (ModelState.IsValid)
        {
            var updatedUser = await _userService.UpdateUser(updateModel);
            if (updatedUser != null)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid edit attempt.");
        }

        return View(updateModel); // Pass the updateModel back to the view if the model state is invalid
    }

    [HttpDelete("User/Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deletedUser = await _userService.DeleteUser(id);
        if (deletedUser != null)
        {
            return View();
        }

        return NotFound();
    }

    [HttpGet("User/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }

        return Json(user);
    }

    // logout
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutUser();
        return RedirectToAction("Index");
    }
}