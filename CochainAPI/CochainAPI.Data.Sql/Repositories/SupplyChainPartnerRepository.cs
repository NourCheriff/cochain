using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class SupplyChainPartnerRepository : SqlRepository, ISupplyChainPartnerRepository
    {
        public SupplyChainPartnerRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<SupplyChainPartnerType?>> GetTypes()
        {
            return await dbContext.SupplyChainPartnerType.ToListAsync();
        }
    }
}
