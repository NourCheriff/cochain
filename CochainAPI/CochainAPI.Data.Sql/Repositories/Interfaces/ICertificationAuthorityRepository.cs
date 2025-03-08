using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICertificationAuthorityRepository
    {
        Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id);
        Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<bool> DeleteSustainabilityCertificate(Guid documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId);
    }
}
