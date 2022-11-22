using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace DemoSite.Repositories;

public class DatabaseRepositoryBase
{
    protected ApplicationDbContext DbContext;

    protected DatabaseRepositoryBase(ApplicationDbContext context)
    {
        DbContext = context;
    }
    protected async Task WithTransaction(WithTransactionDelegate func)
    {
        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await DbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
            await func(transaction);
            await transaction.CommitAsync();
        });
    }

    protected delegate Task WithTransactionDelegate(IDbContextTransaction transaction);
}