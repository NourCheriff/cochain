using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class SupplyChainPartnerRepository : SqlRepository, ISupplyChainPartnerRepository
    {
        public SupplyChainPartnerRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id)
        {            
            return await dbContext.SupplyChainPartner.FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<List<SupplyChainPartnerType>> GetTypes()
        {
            return await dbContext.SupplyChainPartnerType.ToListAsync();
        }
    }
}
