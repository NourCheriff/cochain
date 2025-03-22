
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ISupplyChainPartnerCertificateRepository : IBaseDocumentRepository
    {
        Task<SupplyChainPartnerCertificate?> AddDocument(SupplyChainPartnerCertificate documentObj);
        Task<Page<SupplyChainPartnerCertificate>> GetSustainabilityCertificates(string? queryParam, int? pageNumber, int? pageSize);
    }
}
