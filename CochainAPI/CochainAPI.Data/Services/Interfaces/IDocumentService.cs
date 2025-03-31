
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<BaseDocument?> AddDocument(BaseDocument documentObj);
        Task<BaseDocument?> AddCertificate(BaseDocument documentObj);
        Task<BaseDocument?> GetById(string id, string Type);
        Task<bool> DeleteById(Guid id, string Type);
        Task<bool> DeleteCertificateById(Guid id, string Type);
        Task<Page<SupplyChainPartnerCertificate>> GetSustainabilityCertificates(string? queryParam, int? pageNumber, int? pageSize);
        Task<Page<Contract>?> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize);
        Task<Page<Contract>?> GetReceivedContracts(string scpId, string? queryParam, int? pageNumber, int? pageSize);
    }
}
