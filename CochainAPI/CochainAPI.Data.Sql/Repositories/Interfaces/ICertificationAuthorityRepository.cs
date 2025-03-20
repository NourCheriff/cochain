using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICertificationAuthorityRepository
    {
        Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id);
        Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<bool> DeleteSustainabilityCertificate(Guid documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId);
        Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority);
    }
}
