using CochainAPI.Data.Helpers;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Helper;

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

        public async Task<List<SupplyChainPartnerType>> GetTypes()
        {
            return await _supplyChainPartnerRepository.GetTypes();
        }

        public async Task<Page<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize)
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

            return await _supplyChainPartnerRepository.GetSupplyChainPartners(queryParam, number, size);            
        }

        public async Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner)
        {
            if (!supplyChainPartner.Email.IsValidEmail())
                return null;
            
            if (supplyChainPartner.WalletId == null)
                return null;
            
            var supplyChainPartnerType = await _supplyChainPartnerRepository.GetTypeById(supplyChainPartner.SupplyChainPartnerTypeId);
            if (supplyChainPartnerType == null)
                return null;

            var result = await _supplyChainPartnerRepository.GetSupplyChainPartnerByWalletId(supplyChainPartner.WalletId);
            if (result != null)
                return null;

            supplyChainPartner.Credits = 0.0F;

            return await _supplyChainPartnerRepository.AddSupplyChainPartner(supplyChainPartner);
        }
        
        public async Task<bool> UpdateScpCredits(Guid scpId, float credits)
        {
            if (Guid.TryParse(scpId.ToString(), out var id) && float.TryParse(credits.ToString(), out var deltaCredits))
            {
                return await _supplyChainPartnerRepository.UpdateScpCredits(id, deltaCredits);
            }

            return false;
        }
    }
}
