using AuthBlog.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using AuthBlog.DTO;
using AuthBlog.Models;
using AuthBlog.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

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
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Authorize(Roles = "admin")]
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

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Authorize]
  [HttpGet("me")]
  public async Task<IActionResult> GetMe()
  {
    try
    {
      var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

      var result = await _userService.GetUserByEmailAsync(email!);
      return Ok(result);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }
}