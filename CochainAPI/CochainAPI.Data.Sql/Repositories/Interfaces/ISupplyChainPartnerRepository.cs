using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ISupplyChainPartnerRepository
    {
        Task<List<SupplyChainPartnerType>> GetTypes();
        Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id);
        Task<Page<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize);
        Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner);
        Task<bool> UpdateScpCredits(Guid scpId, float credits);
    }
}
