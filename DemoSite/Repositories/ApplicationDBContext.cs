using DemoSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSite.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public required DbSet<User> Users { get; init; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(u => u.UserData).AutoInclude();
        }
    }
}
