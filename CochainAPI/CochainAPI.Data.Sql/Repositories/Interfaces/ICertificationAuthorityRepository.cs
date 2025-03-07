
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICertificationAuthorityRepository
    {
        Task<IEnumerable<SupplyChainPartnerCertificate?>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<Guid?> DeleteSustainabilityCertificate(Guid documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId);
    }
}
