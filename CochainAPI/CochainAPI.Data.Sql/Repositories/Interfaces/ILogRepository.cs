using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<bool> AddLog(Log log);
    }
}
