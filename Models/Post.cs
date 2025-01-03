using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthBlog.Models;

public class Post
{
  public int PostId { get; set; }

  [Required(ErrorMessage = "Title is required")]
  public string? Title { get; set; }

  [Required(ErrorMessage = "Content is required")]
  public string? Content { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;

  [Required(ErrorMessage = "UserId is required")]
  public int UserId { get; set; }

  [JsonIgnore]
  public virtual User User { get; set; } = null!;
}