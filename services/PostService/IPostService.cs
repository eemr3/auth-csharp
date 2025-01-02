using AuthBlog.Models;

namespace AuthBlog.Services.PostService;

public interface IPostService
{
  Task<Post> CreatePost(Post post);
  Task<IEnumerable<Post>> GetPosts();
  Task<Post> GetPost(int id);
  Task<Post> UpdatePost(Post post);
  void DeletePost(int id);
}