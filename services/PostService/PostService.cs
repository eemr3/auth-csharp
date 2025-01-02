using AuthBlog.Models;
using AuthBlog.Repositories;

namespace AuthBlog.Services.PostService;

public class PostService : IPostService
{
  private readonly IPostRepository _postRepository;

  public PostService(IPostRepository postRepository)
  {
    _postRepository = postRepository;
  }

  public async Task<Post> CreatePost(Post post)
  {
    return await _postRepository.CreatePost(post);
  }

  public void DeletePost(int id)
  {
    _postRepository.DeletePost(id);
  }

  public async Task<Post> GetPost(int id)
  {
    return await _postRepository.GetPostById(id);
  }

  public async Task<IEnumerable<Post>> GetPosts()
  {
    return await _postRepository.GetAllPosts();
  }

  public async Task<Post> UpdatePost(Post post)
  {
    return await _postRepository.UpdatePost(post);
  }
}