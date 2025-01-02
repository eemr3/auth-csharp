using AuthBlog.DTO;
using AuthBlog.Models;

namespace AuthBlog.Repositories;

public interface IUserRepository
{
  public Task<User> AddUserAsync(User user);
  public Task<User?> GetUserByEmailAsync(string email);
  public Task<UserDTOResponse> GetUserByIdAsync(int id);
  public Task CommitAsync();
}