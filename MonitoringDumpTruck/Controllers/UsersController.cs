using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using MonitoringDumpTruck.Helpers;
using MonitoringDumpTruck.Models;
using MonitoringDumpTruck.Models.Entities;
using System.Security.Claims;
using System.Text;

namespace MonitoringDumpTruck.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MonitoringContext _context;

    public UsersController(MonitoringContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] User model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(model.Password);

        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });


        User user = new User()
        {
            Email = model.Email,
            Name = model.Name,
            Password = passwordHash,
            RoleId = 1
        };

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", 
                    Message = "User creation failed! Please check user details and try again." });
        }

    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null)
            return Unauthorized();
       
        var userPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

        if (user == null && userPassword)
            return Unauthorized("Email not found and/or password incorrect");

        if (user != null && userPassword)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return Ok(new Response { Status = "Success", Message = "User sign in successfully!" });
        }
        return Unauthorized();
    }

}
