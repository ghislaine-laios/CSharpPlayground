using DemoSite.Models.Domain;
using Microsoft.EntityFrameworkCore;
using File = DemoSite.Models.Domain.File;

namespace DemoSite.Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public required DbSet<User> Users { get; init; }
    public required DbSet<File> Files { get; init; }
    public required DbSet<Post> Posts { get; init; }
    public required DbSet<Comment> Comments { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Navigation(u => u.UserData).AutoInclude();
        modelBuilder.Entity<File>().HasOne<User>().WithMany().HasForeignKey(f => f.UserId);
        modelBuilder.Entity<Post>().HasOne<User>().WithMany().HasForeignKey(p => p.UserId);
        modelBuilder.Entity<Comment>().HasOne<User>().WithMany().HasForeignKey(c => c.SenderId);
        modelBuilder.Entity<Comment>().HasOne<Post>().WithMany().HasForeignKey(c => c.PostId);
    }
}