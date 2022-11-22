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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Navigation(u => u.UserData).AutoInclude();
        modelBuilder.Entity<File>().HasOne<User>().WithMany().HasForeignKey(f => f.UserId);
    }
}