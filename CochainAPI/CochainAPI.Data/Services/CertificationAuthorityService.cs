using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize)
        {

            return await _certificationAuthorityRepository.GetCertificationAuthorities(queryParam, pageNumber, pageSize);
        }

        public async Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out var caId))
            {
                return await _certificationAuthorityRepository.GetCertificationAuthorityById(caId);
            }
            return null;
        }
    }
}
