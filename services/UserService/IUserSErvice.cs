using AuthBlog.DTO;
using AuthBlog.Models;


namespace AuthBlog.Services.UserService;

public interface IUserService
{
  public Task<UserDTOResponse> AddUserAsync(User user);
  public Task<UserDTOResponse> GetUserAsync(int userId);
  public Task<User> GetUserByEmailAsync(string email);
}
