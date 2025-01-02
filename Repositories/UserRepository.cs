using AuthBlog.Data;
using AuthBlog.DTO;
using AuthBlog.Exceptions;
using AuthBlog.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthBlog.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }
  public async Task<UserDTOResponse> AddUserAsync(User user)
  {
    var addUser = await _context.Users.AddAsync(user);

    return new UserDTOResponse
    {
      UserId = addUser.Entity.UserId,
      Name = addUser.Entity.Name,
      Email = addUser.Entity.Email,
      Role = addUser.Entity.Role
    };
  }

  public async Task<User> GetUserByEmailAsync(string email)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (user == null)
    {
      throw new UnauthorizedException("Email address or password provided is incorrect.");
    }

    return user;
  }
  public async Task<UserDTOResponse> GetUserByIdAsync(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null) throw new KeyNotFoundException($"User with id {id} not found");

    return new UserDTOResponse
    {
      UserId = user.UserId,
      Name = user.Name,
      Email = user.Email,
      Role = user.Role
    };
  }

  public Task CommitAsync()
  {
    return _context.SaveChangesAsync();
  }
}