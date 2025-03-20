using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Sql.Repositories
{
    public class LogRepository : SqlRepository, ILogRepository
    {
        public LogRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> AddLog(Log log)
        {
            await dbContext.AddAsync(log);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
