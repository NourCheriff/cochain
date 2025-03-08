using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICertificationAuthorityRepository
    {
        Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<Guid?> DeleteSustainabilityCertificate(Guid documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId);
    }
}
