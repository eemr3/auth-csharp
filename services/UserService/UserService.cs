using AuthBlog.DTO;
using AuthBlog.Exceptions;
using AuthBlog.Models;
using AuthBlog.Repositories;

namespace AuthBlog.Services.UserService;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserDTOResponse> AddUserAsync(User user)
  {
    var userExists = await _userRepository.GetUserByEmailAsync(user.Email!);
    if (userExists != null)
    {
      throw new ConflictException($"User with this {user.Email} already exists");
    }

    var userCreated = await _userRepository.AddUserAsync(user);

    await _userRepository.CommitAsync();

    return new UserDTOResponse
    {
      UserId = userCreated.UserId,
      Name = userCreated.Name,
      Email = userCreated.Email,
      Role = userCreated.Role
    };
  }

  public async Task<UserDTOResponse> GetUserAsync(int userId)
  {
    return await _userRepository.GetUserByIdAsync(userId);
  }

  public async Task<User> GetUserByEmailAsync(string email)
  {
    var userExists = await _userRepository.GetUserByEmailAsync(email);

    if (userExists == null)
    {
      throw new UnauthorizedException($"User with this {email} does not exist");
    }

    return userExists;
  }
}