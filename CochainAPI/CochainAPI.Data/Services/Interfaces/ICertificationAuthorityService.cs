using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICertificationAuthorityService
    {
        Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id);
        Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<bool> DeleteSustainabilityCertificate(string documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(string documentId);
        Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority);
    }
}
