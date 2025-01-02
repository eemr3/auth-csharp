using AuthBlog.DTO;
using AuthBlog.Exceptions;
using AuthBlog.Services.Token;
using AuthBlog.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AuthBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly TokenGenerator _tokenGenerator;

  public AuthController(IUserService userService, IConfiguration configuration)
  {
    _userService = userService;
    _tokenGenerator = new TokenGenerator(configuration);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDTORequest loginRequest)
  {
    try
    {
      var user = await _userService.GetUserByEmailAsync(loginRequest.Email!);
      if (user.Password != loginRequest.Password)
      {
        return Unauthorized(new { message = "Email address or password provided is incorrect." });
      }
      var Token = _tokenGenerator.Generator(user);

      return Ok(new { asscess_token = Token });
    }
    catch (UnauthorizedException)
    {
      return Unauthorized(new { message = "Email address or password provided is incorrect." });
    }
  }
}