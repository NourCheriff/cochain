using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICertificationAuthorityRepository
    {
        Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id);
        Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority);
    }
}