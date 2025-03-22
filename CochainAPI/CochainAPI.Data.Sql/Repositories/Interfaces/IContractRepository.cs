
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IContractRepository : IBaseDocumentRepository
    {
        Task<Contract> AddDocument(Contract documentObj);
        Task<Page<Contract>> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize);
        Task<Page<Contract>> GetReceivedContracts(Guid scpId, string? queryParam, int? pageNumber, int? pageSize);
    }
}
