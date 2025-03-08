using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICertificationAuthorityService
    {
        Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate?>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<Guid?> DeleteSustainabilityCertificate(string documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(string documentId);
    }
}
