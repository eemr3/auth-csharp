namespace AuthBlog.Models;

public class Post
{
  public int PostId { get; set; }
  public string Title { get; set; } = null!;
  public string Content { get; set; } = null!;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public int UserId { get; set; }
  public User User { get; set; } = null!;
}