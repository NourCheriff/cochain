using Microsoft.EntityFrameworkCore.Storage;

namespace CochainAPI.Data.Sql.Repositories
{
    public abstract class SqlRepository
    {
        protected readonly CochainDBContext dbContext;

        public SqlRepository(CochainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> BeginTransaction<T>(Func<IDbContextTransaction, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            return await action(transaction).ConfigureAwait(false);
        }
    }
}
