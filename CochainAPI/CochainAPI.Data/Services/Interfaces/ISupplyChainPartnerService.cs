using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ISupplyChainPartnerService
    {
        Task<List<SupplyChainPartnerType?>> GetTypes();
    }
}
