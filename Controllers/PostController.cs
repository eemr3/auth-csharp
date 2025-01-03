using System.Security.Claims;
using AuthBlog.DTO;
using AuthBlog.Models;
using AuthBlog.Services.PostService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
  private readonly IPostService _postService;

  public PostController(IPostService postService)
  {
    _postService = postService;
  }

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Authorize(Policy = "adminOrEditor")]
  [HttpPost]
  public async Task<IActionResult> CreatePost([FromBody] ProductDTORequest request)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var post = new Post
    {
      Title = request.Title,
      Content = request.Content,
      UserId = Convert.ToInt32(userId)
    };
    var result = await _postService.CreatePost(post);
    return CreatedAtAction(nameof(GetPost), new { postId = result.PostId }, result);
  }

  [HttpGet]
  public async Task<IActionResult> GetPosts()
  {
    var posts = await _postService.GetPosts();
    return Ok(posts);
  }

  [HttpGet("{postId}")]
  public async Task<IActionResult> GetPost(int postId)
  {
    try
    {

      var post = await _postService.GetPost(postId);

      return Ok(post);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Authorize(Policy = "adminOrEditor")]
  [HttpPut("{postId}")]
  public async Task<IActionResult> UpdatePost(int postId, [FromBody] ProductDTORequest request)
  {
    var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
    var post = new Post
    {
      PostId = postId,
      Title = request.Title,
      Content = request.Content,
      UserId = Convert.ToInt32(userId)
    };
    try
    {
      var result = await _postService.UpdatePost(post);
      return Ok(result);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Authorize(Policy = "admin")]
  [HttpDelete("{postId}")]
  public IActionResult DeletePost(int postId)
  {
    try
    {
      _postService.DeletePost(postId);
      return NoContent();
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { message = ex.Message });
    }
  }
}