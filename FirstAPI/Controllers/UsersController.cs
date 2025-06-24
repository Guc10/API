using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller
{
    static private List<User> _users = new List<User>
    {
        new User
        {
            Id = 1,
            Username = "admin",
            Password = "admin",
            Age = 13
        },
        new User
        {
            Id = 2,
            Username = "nonAdmin",
            Password = "admin",
            Age = 25
        }
    };

    [HttpGet]
    public ActionResult<List<User>> GetUsers()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> AddUser(User user)
    {
        if(user == null)
            return BadRequest();
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        if (user == null)
            return NotFound();
        
        user.Id = updatedUser.Id;
        user.Username = updatedUser.Username;
        user.Password = updatedUser.Password;
        user.Age = updatedUser.Age;

        return NoContent();
    }
}