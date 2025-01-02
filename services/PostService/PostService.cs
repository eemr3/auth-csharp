using AuthBlog.DTO;
using AuthBlog.Mappers;
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

  public async Task<PostDTOResponse> CreatePost(Post post)
  {
    var newPost = await _postRepository.CreatePost(post);
    await _postRepository.CommitAsync();

    var result = PostMapper.ToPostResponseDTO(newPost);
    return result;
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