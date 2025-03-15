
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<BaseDocument?> AddDocument(BaseDocument documentObj);
        Task<BaseDocument?> GetById(string id, string Type);
        Task<bool> DeleteById(Guid id, string filename, string Type);
        Task<bool> DeleteCertificateById(Guid id, string filename, string Type);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificates(string queryParam, int? pageNumber, int? pageSize); 
        Task<List<Contract>?> GetEmittedContracts(string userId, string queryParam, int? pageNumber, int? pageSize);
        Task<List<Contract>?> GetReceivedContracts(string scpId, string queryParam, int? pageNumber, int? pageSize);
    }
}
