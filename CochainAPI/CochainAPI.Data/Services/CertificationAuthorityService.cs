using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services
{
    public class CertificationAuthorityService : ICertificationAuthorityService
    {
        private readonly ICertificationAuthorityRepository _certificationAuthorityRepository;

        public CertificationAuthorityService(ICertificationAuthorityRepository certificationAuthorityRepository)
        {
            _certificationAuthorityRepository = certificationAuthorityRepository;
        }

        public async Task<List<SupplyChainPartnerCertificate?>> GetSustainabilityCertificate(string certificationAuthorityId)
        {
            return await _certificationAuthorityRepository.GetSustainabilityCertificate(certificationAuthorityId);
        }
        public async Task<Guid?> DeleteSustainabilityCertificate(string documentId)
        {
            try
            {
                Guid parsedDocumentId = Guid.Parse(documentId);
                return await _certificationAuthorityRepository.DeleteSustainabilityCertificate(parsedDocumentId);
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

        public Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
