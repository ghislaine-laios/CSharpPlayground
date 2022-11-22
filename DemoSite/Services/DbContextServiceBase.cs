using System.Data;
using DemoSite.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace DemoSite.Services
{
    public class DbContextServiceBase
    {
        private readonly ApplicationDbContext _dbContext;

        protected DbContextServiceBase(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        protected async Task<T> WithTransaction<T>(WithTransactionDelegate<T> func, bool autoRetry = true)
        {
            if (!autoRetry) throw new NotImplementedException();
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction =
                    await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
                var result = await func(transaction);
                await transaction.CommitAsync();
                return result;
            });
        }

        protected delegate Task<T> WithTransactionDelegate<T>(IDbContextTransaction transaction);
    }
}
