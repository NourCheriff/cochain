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
            return await dbContext.SupplyChainPartner.Include(x => x.SupplyChainPartnerType).FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<List<SupplyChainPartnerType>> GetTypes()
        {
            return await dbContext.SupplyChainPartnerType.ToListAsync();
        }

        public async Task<List<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.SupplyChainPartner.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)));

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            query = query.Include(x => x.SupplyChainPartnerType);

            return await query.ToListAsync();
        }

        public async Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner)
        {
            var savedSupplyChainPartner = await dbContext.SupplyChainPartner.AddAsync(supplyChainPartner);
            await dbContext.SaveChangesAsync();
            supplyChainPartner.Id = savedSupplyChainPartner.Entity.Id;
            return supplyChainPartner;
        }
        
        public async Task<bool> UpdateScpCredits(Guid scpId, float credits)
        {
            return await dbContext.SupplyChainPartner.Where(s => s.Id == scpId)
                                                    .ExecuteUpdateAsync(s => s.SetProperty(
                                                        scp => scp.Credits,
                                                        scp => scp.Credits + credits)) > 0;
        }
    }
}
