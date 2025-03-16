using CochainAPI.Data.Services.Interfaces;
using Quartz;

namespace CochainAPI.Jobs
{
    public class TokenProcessor : IJob
    {
        private readonly IProductLifeCycleService _lifeCycleService;
        private readonly ISupplyChainPartnerService _supplyChainPartnerService;
        private readonly ICarbonOffsettingActionService _actionService;

        public TokenProcessor(IProductLifeCycleService lifeCycleService, ISupplyChainPartnerService supplyChainPartnerService, ICarbonOffsettingActionService actionService)
        {
            _lifeCycleService = lifeCycleService;
            _supplyChainPartnerService = supplyChainPartnerService;
            _actionService = actionService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var productLifeCycle = await _lifeCycleService.GetProductLifeCyclesToBeProcessed();
            foreach (var item in productLifeCycle)
            {
                var credits = item.SupplyChainPartner.SupplyChainPartnerType.Baseline - item.Emissions;
                //transaction with blockchain
                var transactionId = "";
                item.EmissionTransactionId = transactionId;
                var updateResult = await _supplyChainPartnerService.UpdateScpCredits(item.SupplyChainPartnerId, credits);
                if (!updateResult)
                {
                    item.IsEmissionProcessed = true;
                    await _lifeCycleService.SaveProductLife(item);
                }
            }

            var offsettingActions = await _actionService.GetOffsettingActionsToBeProcessed();
            foreach(var item in offsettingActions)
            {
                //transaction with blockchain item.Offset
                var transactionId = "";
                item.EmissionTransactionId = transactionId;
                var updateResult = await _supplyChainPartnerService.UpdateScpCredits(item.SupplyChainPartnerId, item.Offset);
                if (!updateResult)
                {
                    item.IsProcessed = true;
                    await _actionService.SaveCarbonOffsettingAction(item);
                }
            }
        }
    }
}
