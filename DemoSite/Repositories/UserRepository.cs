using System.Data;
using DemoSite.Models.Domain;
using DemoSite.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoSite.Repositories;

public class UserRepository : DatabaseRepositoryBase, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext): base(dbContext) {}

    public async Task<User> Import(long id)
    {
        return await DbContext.Users.SingleAsync(u => u.Id == id);
    }

    public async Task<User> Import(string username)
    {
        return await DbContext.Users.SingleAsync(u => u.Username == username);
    }

    public async Task<long> Export(User user)
    {
        var anyUser = await DbContext.Users.AnyAsync(u => u.Username == user.Username);
        if (anyUser) throw new UsernameConflictException();
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task Update(User user)
    {
        await DbContext.SaveChangesAsync();
    }
}