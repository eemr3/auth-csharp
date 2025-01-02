using System.Runtime.CompilerServices;
using AuthBlog.Models;

namespace AuthBlog.Repositories;

public interface IPostRepository
{
  Task<Post> CreatePost(Post post);
  Task<IEnumerable<Post>> GetAllPosts();
  Task<Post> GetPostById(int id);
  Task<Post> UpdatePost(Post post);
  void DeletePost(int id);

  public Task CommitAsync();
}