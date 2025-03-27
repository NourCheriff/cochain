using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICertificationAuthorityService
    {
        Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize);
        Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority);
    }
}
