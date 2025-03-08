using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services
{
    public class SupplychainPartnerService : ISupplyChainPartnerService
    {
        private readonly ISupplyChainPartnerRepository _supplyChainPartnerRepository;

        public SupplychainPartnerService(ISupplyChainPartnerRepository supplyChainPartnerRepository, ISupplyChainPartnerCertificateRepository supplyChainPartnerCertificateRepository, IProductLifeCycleDocumentRepository productLifeCycleDocumentRepository)
        {
            _supplyChainPartnerRepository = supplyChainPartnerRepository;
        }

        public async Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out var scpId))
            {
                return await _supplyChainPartnerRepository.GetSupplyChainPartnerById(scpId);
            }
            return null;
        }

        public async Task<List<SupplyChainPartnerType?>> GetTypes()
        {
            return await _supplyChainPartnerRepository.GetTypes();
        }
    }
}
