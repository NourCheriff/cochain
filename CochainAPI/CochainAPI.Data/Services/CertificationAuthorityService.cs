using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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

        public async Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId)
        {
            return await _certificationAuthorityRepository.GetSustainabilityCertificate(certificationAuthorityId);
        }
        public async Task<bool> DeleteSustainabilityCertificate(string documentId)
        {
            try
            {
                if (Guid.TryParse(documentId, out var parsedDocumentId))
                {
                    return await _certificationAuthorityRepository.DeleteSustainabilityCertificate(parsedDocumentId);
                }
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public async Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(string documentId)
        {
            try
            {
                Guid parsedDocumentId = Guid.Parse(documentId);
                return await _certificationAuthorityRepository.UpdateSustainabilityCertificate(parsedDocumentId);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
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

        public async Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out var caId))
            {
                return await _certificationAuthorityRepository.GetCertificationAuthorityById(caId);
            }
            return null;
        }

        public async Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority)
        {
            if (!certificationAuthority.Email.IsValidEmail())
                return null;
            
            return await _certificationAuthorityRepository.AddCertificationAuthority(certificationAuthority);
        }
    }
}
