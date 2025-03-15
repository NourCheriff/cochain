
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ISupplyChainPartnerCertificateRepository : IBaseDocumentRepository
    {
        Task<SupplyChainPartnerCertificate?> AddDocument(SupplyChainPartnerCertificate documentObj);
        Task<List<SupplyChainPartnerCertificate>?> GetSustainabilityCertificates(string queryParam, int? pageNumber, int? pageSize);
    }
}
