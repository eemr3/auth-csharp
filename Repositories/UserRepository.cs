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
  public async Task<User> AddUserAsync(User user)
  {
    var addUser = await _context.Users.AddAsync(user);
    Console.WriteLine(addUser.Entity);

    return addUser.Entity;
  }

  public async Task<User?> GetUserByEmailAsync(string email)
  {
    var user = await _context.Users.Where(u => u.Email == email).Include(c => c.Posts).FirstOrDefaultAsync();

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

  public async Task CommitAsync()
  {
    await _context.SaveChangesAsync();
  }
}