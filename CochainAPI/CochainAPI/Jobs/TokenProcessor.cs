using CochainAPI.Data.Services.Interfaces;
using Quartz;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;

namespace CochainAPI.Jobs
{
    public class TokenProcessor : IJob
    {
        private readonly IProductLifeCycleService _lifeCycleService;
        private readonly ISupplyChainPartnerService _supplyChainPartnerService;
        private readonly ICarbonOffsettingActionService _actionService;
        private readonly string _blockchainURL;

        public TokenProcessor(IProductLifeCycleService lifeCycleService, ISupplyChainPartnerService supplyChainPartnerService, ICarbonOffsettingActionService actionService, string blockchainURL)
        {
            _lifeCycleService = lifeCycleService;
            _supplyChainPartnerService = supplyChainPartnerService;
            _actionService = actionService;
            _blockchainURL = blockchainURL;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var productLifeCycles = await _lifeCycleService.GetProductLifeCyclesToBeProcessed();

            foreach (var item in productLifeCycles)
            {
                var walletId = item.SupplyChainPartner.WalletId;
                var account = new Account(walletId);
                var web3 = new Web3(account, _blockchainURL);
                var credits = item.SupplyChainPartner.SupplyChainPartnerType.Baseline - item.Emissions;
                var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(walletId, (decimal)credits);

                var transactionId = transaction.TransactionHash;
                item.EmissionTransactionId = transactionId;

                var updateResult = await _supplyChainPartnerService.UpdateScpCredits(item.SupplyChainPartnerId, credits);

                if (!updateResult)
                {
                    item.IsEmissionProcessed = true;
                    await _lifeCycleService.SaveProductLife(item);
                }
            }

            var offsettingActions = await _actionService.GetOffsettingActionsToBeProcessed();
            foreach (var item in offsettingActions)
            {
                var walletId = item.SupplyChainPartner.WalletId;
                var account = new Account(walletId);
                var web3 = new Web3(account, _blockchainURL);
                var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(walletId, (decimal)item.Offset);
                var transactionId = transaction.TransactionHash;
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
