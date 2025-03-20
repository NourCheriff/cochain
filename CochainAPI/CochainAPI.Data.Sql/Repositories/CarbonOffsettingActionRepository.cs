using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CarbonOffset;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CarbonOffsettingActionRepository : SqlRepository, ICarbonOffsettingActionRepository
    {
        private readonly ILogRepository logRepository;
        public CarbonOffsettingActionRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<CarbonOffsettingAction> AddCarbonOffsettingAction(CarbonOffsettingAction action)
        {
            var savedAction = await dbContext.CarbonOffsettingAction.AddAsync(action);
            await dbContext.SaveChangesAsync();
            action.Id = savedAction.Entity.Id;
            return action;
        }

        public async Task<List<CarbonOffsettingAction>> GetCarbonOffsettingActions(string? scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.CarbonOffsettingAction.Where(x => (x.SupplyChainPartner != null && x.SupplyChainPartner.Name != null && (queryParam == null || x.SupplyChainPartner.Name.Contains(queryParam))) && (x.SupplyChainPartnerId != null && (scpId == null || x.SupplyChainPartnerId.ToString().Contains(scpId))));

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed()
        {
            return await dbContext.CarbonOffsettingAction.Where(s => !s.IsProcessed).ToListAsync();
        }

        public async Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction)
        {
            dbContext.CarbonOffsettingAction.Update(carbonOffsettingAction);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}