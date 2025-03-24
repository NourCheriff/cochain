using CochainAPI.Model.Helper;
using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<Page<Log>> GetLogs(string? severity, string? queryParam, int? pageNumber, int? pageSize);
        Task<bool> AddLog(Log log);
    }
}
