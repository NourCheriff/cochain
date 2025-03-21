using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Helper;
using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository) 
        {
            _logRepository = logRepository;
        }
        public async Task<Page<Log>> GetLogs(string? severity, string? queryParam, int? pageNumber, int? pageSize)
        {
            return await _logRepository.GetLogs(severity, queryParam, pageNumber, pageSize);
        }
    }
}
