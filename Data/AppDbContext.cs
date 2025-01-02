using AuthBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthBlog.Data;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Post> Posts { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>().ToTable("users");
    modelBuilder.Entity<User>().HasKey(u => u.UserId);
    modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("user_id");
    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name");
    modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
    modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");
    modelBuilder.Entity<User>().Property(u => u.Role).HasColumnName("role");

    modelBuilder.Entity<Post>().ToTable("posts");
    modelBuilder.Entity<Post>().HasKey(p => p.PostId);
    modelBuilder.Entity<Post>().Property(p => p.PostId).HasColumnName("post_id");
    modelBuilder.Entity<Post>().HasOne(p => p.User).WithMany(u => u.Posts).HasForeignKey(p => p.UserId);
    modelBuilder.Entity<Post>().Property(p => p.Title).HasColumnName("title");
    modelBuilder.Entity<Post>().Property(p => p.Content).HasColumnName("content");
    modelBuilder.Entity<Post>().Property(p => p.CreatedAt).HasColumnName("created_at");
    modelBuilder.Entity<Post>().Property(p => p.UserId).HasColumnName("user_id");
  }
}