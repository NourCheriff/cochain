using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CarbonOffset;
using CochainAPI.Model.Helper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CochainAPI.Data.Services
{
    public class CarbonOffsettingActionService : ICarbonOffsettingActionService
    {
        private readonly ICarbonOffsettingActionRepository _actionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public CarbonOffsettingActionService(ICarbonOffsettingActionRepository actionService, IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _actionRepository = actionService;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public async Task<CarbonOffsettingAction?> AddCarbonOffsettingAction(CarbonOffsettingAction action)
        {
            var userId = _contextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.GetById(userId);
            action.SupplyChainPartnerId = user!.SupplyChainPartnerId.GetValueOrDefault();

            return await _actionRepository.AddCarbonOffsettingAction(action);
        }

        public async Task<Page<CarbonOffsettingAction>> GetCarbonOffsettingActions(string? scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            return await _actionRepository.GetCarbonOffsettingActions(scpId, queryParam, pageNumber, pageSize);
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
