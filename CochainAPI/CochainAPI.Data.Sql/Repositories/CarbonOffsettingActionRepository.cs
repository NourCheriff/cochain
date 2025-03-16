using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CarbonOffset;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CarbonOffsettingActionRepository : SqlRepository, ICarbonOffsettingActionRepository
    {
        public CarbonOffsettingActionRepository(CochainDBContext dbContext) : base(dbContext)
        {
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