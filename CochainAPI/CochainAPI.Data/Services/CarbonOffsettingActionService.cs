using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CarbonOffset;

namespace CochainAPI.Data.Services
{
    public class CarbonOffsettingActionService : ICarbonOffsettingActionService
    {
        private readonly ICarbonOffsettingActionRepository _actionRepository;
        public CarbonOffsettingActionService(ICarbonOffsettingActionRepository actionService)
        {
            _actionRepository = actionService;
        }

        public async Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed()
        {
            return await _actionRepository.GetOffsettingActionsToBeProcessed();
        }

        public async Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction)
        {
            return await _actionRepository.SaveCarbonOffsettingAction(carbonOffsettingAction);
        }
    }
}
