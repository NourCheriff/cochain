using CochainAPI.Model.CarbonOffset;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICarbonOffsettingActionService
    {
        public Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed();
        public Task<CarbonOffsettingAction?> AddCarbonOffsettingAction(CarbonOffsettingAction action);
        public Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction);
    }
}
