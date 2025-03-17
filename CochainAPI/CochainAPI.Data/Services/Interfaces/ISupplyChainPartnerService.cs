using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ISupplyChainPartnerService
    {
        Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id);
        Task<List<SupplyChainPartnerType>> GetTypes();
        Task<List<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize);
        Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner);
    }
}
