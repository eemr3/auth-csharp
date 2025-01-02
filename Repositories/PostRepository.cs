using AuthBlog.Data;
using AuthBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthBlog.Repositories;

public class PostRepository : IPostRepository
{
  private readonly AppDbContext _context;

  public PostRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Post> CreatePost(Post post)
  {
    var newPost = await _context.Posts.AddAsync(post);

    return newPost.Entity;
  }

  public async void DeletePost(int id)
  {
    var post = await GetPostById(id);

    _context.Posts.Remove(post);

  }

  public async Task<IEnumerable<Post>> GetAllPosts()
  {
    return await _context.Posts.ToListAsync();
  }

  public async Task<Post> GetPostById(int id)
  {
    var post = await _context.Posts.FindAsync(id);

    if (post == null)
    {
      throw new KeyNotFoundException("Post not found");
    }

    return post;
  }

  public async Task<Post> UpdatePost(Post post)
  {
    await GetPostById(post.PostId);

    _context.Posts.Update(post);

    return post;
  }

  public async Task CommitAsync()
  {
    await _context.SaveChangesAsync();
  }
}