using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ISupplyChainPartnerRepository
    {
        Task<List<SupplyChainPartnerType?>> GetTypes();
        Task<SupplyChainPartner?> GetById(Guid id);
    }
}
