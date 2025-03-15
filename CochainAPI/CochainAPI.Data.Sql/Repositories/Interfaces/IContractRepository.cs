
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IContractRepository : IBaseDocumentRepository
    {
        Task<Contract?> AddDocument(Contract documentObj);
        Task<List<SupplyChainPartnerCertificate>?> GetEmittedContracts(string userId, string queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>?> GetReceivedContracts(string scpId, string queryParam, int? pageNumber, int? pageSize);
    }
}
