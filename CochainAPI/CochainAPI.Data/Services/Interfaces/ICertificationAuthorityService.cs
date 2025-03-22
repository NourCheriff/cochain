using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICertificationAuthorityService
    {
        Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id);
        Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<bool> DeleteSustainabilityCertificate(string documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(string documentId);
        Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority);
    }
}
