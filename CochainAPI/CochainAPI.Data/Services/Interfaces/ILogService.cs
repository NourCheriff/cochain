using CochainAPI.Model.Helper;
using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ILogService
    {
        Task<Page<Log>> GetLogs(string? severity, string? queryParam, int? pageNumber, int? pageSize);
    }
}
