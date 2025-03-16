using CochainAPI.Model.CarbonOffset;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ICarbonOffsettingActionRepository
    {
        public Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed();
        public Task<CarbonOffsettingAction> AddCarbonOffsettingAction(CarbonOffsettingAction action);
        public Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction);
    }
}
