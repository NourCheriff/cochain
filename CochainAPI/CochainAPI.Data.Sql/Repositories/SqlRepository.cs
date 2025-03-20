using CochainAPI.Data.Sql.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace CochainAPI.Data.Sql.Repositories
{
    public abstract class SqlRepository
    {
        protected readonly CochainDBContext dbContext;
        protected readonly IHttpContextAccessor httpContextAccessor;

        public SqlRepository(CochainDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
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
