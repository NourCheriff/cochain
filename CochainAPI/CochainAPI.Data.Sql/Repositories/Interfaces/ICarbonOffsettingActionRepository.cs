using CochainAPI.Model.CarbonOffset;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICarbonOffsettingActionRepository
    {
        public Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed();
        public Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction);
    }
}
