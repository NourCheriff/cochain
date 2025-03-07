using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICertificationAuthorityService
    {
        Task<IEnumerable<SupplyChainPartnerCertificate?>> GetSustainabilityCertificate(string certificationAuthorityId);
        Task<Guid?> DeleteSustainabilityCertificate(string documentId);
        Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(string documentId);
    }
}
