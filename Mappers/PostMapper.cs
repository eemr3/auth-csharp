using AuthBlog.DTO;
using AuthBlog.Models;

namespace AuthBlog.Mappers;

public class PostMapper
{
  public static PostDTOResponse ToPostResponseDTO(Post post)
  {
    return new PostDTOResponse
    {
      PostId = post.PostId,
      Title = post.Title!,
      Content = post.Content!,
      CreatedAt = post.CreatedAt,
      UpdatedAt = post.UpdatedAt,
      UserId = post.UserId
    };
  }
}