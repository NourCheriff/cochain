using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ISupplyChainPartnerService
    {
        Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id);
        Task<List<SupplyChainPartnerType>> GetTypes();
        Task<Page<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize);
        Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner);
        Task<bool> UpdateScpCredits(Guid scpId, float credits);
    }
}
