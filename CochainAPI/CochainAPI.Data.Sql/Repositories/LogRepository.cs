using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Helper;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class LogRepository : SqlRepository, ILogRepository
    {
        public LogRepository(CochainDBContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        public async Task<Page<Log>> GetLogs(string? severity, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Log.Include(x => x.User).Where(x => (queryParam == null || (x.Name != null && x.Name.Contains(queryParam))) && (severity == null || x.Severity.Contains(severity)));

            var totalSize = await query.CountAsync();

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            return new Page<Log>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize
            };
        }
        public async Task<bool> AddLog(Log log)
        {
            await dbContext.AddAsync(log);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
