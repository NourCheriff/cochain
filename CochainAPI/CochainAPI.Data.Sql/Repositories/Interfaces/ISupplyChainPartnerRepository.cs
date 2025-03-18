using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ISupplyChainPartnerRepository
    {
        Task<List<SupplyChainPartnerType>> GetTypes();
        Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id);
        Task<List<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize);
        Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner);
        Task<bool> UpdateScpCredits(Guid scpId, float credits);
    }
}
