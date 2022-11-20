using System.Data;
using DemoSite.Models;
using DemoSite.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoSite.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> Import(long id)
    {
        return await _dbContext.Users.SingleAsync(u => u.Id == id);
    }

    public async Task<User> Import(string username)
    {
        return await _dbContext.Users.SingleAsync(u => u.Username == username);
    }

    public async Task<long> Export(User user)
    {
        await WithTransaction(async _ =>
        {
            var anyUser = await _dbContext.Users.AnyAsync(u => u.Username == user.Username);
            if (anyUser) throw new UsernameConflictException();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        });
        return user.Id;
    }

    protected delegate Task WithTransactionDelegate(IDbContextTransaction transaction);

    protected async Task WithTransaction(WithTransactionDelegate func)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
            await func(transaction);
            await transaction.CommitAsync();
        });
    }
}