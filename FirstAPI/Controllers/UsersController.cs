using FirstAPI.Models;
using FirstAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FirstAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _userService.GetAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        var user = await _userService.GetAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        var users = await _userService.GetAsync();
        if (users.Count == 0)
        {
            if(user == null)
                return BadRequest();
            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, User updatedUser)
    {
        var user = await _userService.GetAsync(id);
        if (user == null)
            return NotFound();
        updatedUser.Id = id;
        await _userService.UpdateAsync(id, updatedUser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userService.GetAsync(id);
        if (user == null)
            return NotFound();
        await _userService.RemoveAsync(id);
        return NoContent();
    }
}