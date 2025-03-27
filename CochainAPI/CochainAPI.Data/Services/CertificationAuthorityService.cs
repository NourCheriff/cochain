using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Data.Helpers;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services
{
    public class CertificationAuthorityService : ICertificationAuthorityService
    {
        private readonly ICertificationAuthorityRepository _certificationAuthorityRepository;

        public CertificationAuthorityService(ICertificationAuthorityRepository certificationAuthorityRepository)
        {
            _certificationAuthorityRepository = certificationAuthorityRepository;
        }

        public async Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize)
        {
            int? size = null;
            int? number = null;

            if (pageSize.HasValue && int.TryParse(pageSize.ToString(), out var parsedSize))
            {
                size = parsedSize;
            }

            if (pageNumber.HasValue && int.TryParse(pageNumber.ToString(), out var parsedNumber))
            {
                number = parsedNumber;
            }

            return await _certificationAuthorityRepository.GetCertificationAuthorities(queryParam, number, size);            
        }

        public async Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority)
        {
            if (!certificationAuthority.Email.IsValidEmail())
                return null;
            
            return await _certificationAuthorityRepository.AddCertificationAuthority(certificationAuthority);
        }
    }
}
