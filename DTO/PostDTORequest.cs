namespace AuthBlog.DTO;

public class ProductDTORequest
{
  public string? Title { get; set; }
  public string? Content { get; set; }
  public int UserId { get; set; }
}