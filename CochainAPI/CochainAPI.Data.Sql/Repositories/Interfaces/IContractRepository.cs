
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IContractRepository : IBaseDocumentRepository
    {
        Task<Contract> AddDocument(Contract documentObj);
        Task<List<Contract>> GetEmittedContracts(string userId, string queryParam, int? pageNumber, int? pageSize);
        Task<List<Contract>> GetReceivedContracts(Guid scpId, string queryParam, int? pageNumber, int? pageSize);
    }
}
