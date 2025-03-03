
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Data.Sql.Repositories;

namespace CochainAPI.Data.Services
{
    public class SupplychainPartnerService : ISupplyChainPartnerService
    {
        private readonly ISupplyChainPartnerRepository _supplyChainPartnerRepository;

        public SupplychainPartnerService(ISupplyChainPartnerRepository supplyChainPartnerRepository, ISupplyChainPartnerCertificateRepository supplyChainPartnerCertificateRepository, IProductLifeCycleDocumentRepository productLifeCycleDocumentRepository)
        {
            _supplyChainPartnerRepository = supplyChainPartnerRepository;
        }
        public async Task<List<SupplyChainPartnerType?>> GetTypes()
        {
            return await _supplyChainPartnerRepository.GetTypes();
        }
    }
}
