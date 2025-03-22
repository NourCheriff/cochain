using CochainAPI.Model.CarbonOffset;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ICarbonOffsettingActionService
    {
        public Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed();
        public Task<CarbonOffsettingAction?> AddCarbonOffsettingAction(CarbonOffsettingAction action);
        public Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction);
        public Task<Page<CarbonOffsettingAction>> GetCarbonOffsettingActions(string? scpId, string? queryParam, int? pageNumber, int? pageSize);
    }
}
