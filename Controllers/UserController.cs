using AuthBlog.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using AuthBlog.DTO;
using AuthBlog.Models;
using AuthBlog.Exceptions;

namespace AuthBlog.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  public async Task<IActionResult> RegisterUser([FromBody] UserDTORequest userDTORequest)
  {
    try
    {
      var user = new User
      {
        // Map properties from userDTORequest to user
        Name = userDTORequest.Name,
        Password = userDTORequest.Password,
        Email = userDTORequest.Email,
        Role = userDTORequest.Role

      };
      var result = await _userService.AddUserAsync(user);
      return CreatedAtAction(nameof(GetUser), new { userId = result.UserId }, result);
    }
    catch (ConflictException ex)
    {
      return Conflict(new { message = ex.Message });

    }
  }

  [HttpGet("{userId}")]
  public async Task<IActionResult> GetUser(int userId)
  {
    try
    {
      var result = await _userService.GetUserAsync(userId);
      return Ok(result);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }
}