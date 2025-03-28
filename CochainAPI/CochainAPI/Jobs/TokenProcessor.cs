using CochainAPI.Data.Services.Interfaces;
using Quartz;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using CochainAPI.Data.Sql.Repositories.Interfaces;

namespace CochainAPI.Jobs
{
    public class TokenProcessor : IJob
    {
        private readonly ILogRepository _logRepository;
        private readonly IProductLifeCycleService _lifeCycleService;
        private readonly ISupplyChainPartnerService _supplyChainPartnerService;
        private readonly ICarbonOffsettingActionService _actionService;
        private readonly IConfiguration _config;
        private readonly string _blockchainURL;

        public TokenProcessor(
            IProductLifeCycleService lifeCycleService,
            ISupplyChainPartnerService supplyChainPartnerService,
            ICarbonOffsettingActionService actionService,
            IConfiguration config,
            ILogRepository logRepository)
        {
            _config = config;
            _lifeCycleService = lifeCycleService;
            _supplyChainPartnerService = supplyChainPartnerService;
            _actionService = actionService;
            _logRepository = logRepository;
            _blockchainURL = _config.GetValue<string>("BlockchainSettings:localhostURL")!;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var mainAccount = new Account(_config.GetValue<string>("BlockchainSettings:mainAccountKey")!);
                var web3 = new Web3(mainAccount, _blockchainURL);

                var productLifeCycles = await _lifeCycleService.GetProductLifeCyclesToBeProcessed();
                foreach (var item in productLifeCycles)
                {
                    try
                    {
                        var walletId = item.SupplyChainPartner!.WalletId;
                        var credits = item.SupplyChainPartner.SupplyChainPartnerType!.Baseline - item.Emissions;
                        var transaction = await web3.Eth.GetEtherTransferService()
                                                        .TransferEtherAndWaitForReceiptAsync(
                                                            walletId,
                                                            (decimal)credits,
                                                            gasPriceGwei: 1
                                                        );
                        var transactionId = transaction.TransactionHash;
                        item.EmissionTransactionId = transactionId;

                        var updateResult = await _supplyChainPartnerService.UpdateScpCredits(item.SupplyChainPartnerId, credits);
                        if (updateResult)
                        {
                            item.IsEmissionProcessed = true;
                            await _lifeCycleService.SaveProductLife(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while executing Job: " + ex);
                    }
                }

                var offsettingActions = await _actionService.GetOffsettingActionsToBeProcessed();
                foreach (var item in offsettingActions)
                {
                    try
                    {
                        var walletId = item.SupplyChainPartner!.WalletId;
                        var transaction = await web3.Eth.GetEtherTransferService()
                                                        .TransferEtherAndWaitForReceiptAsync(
                                                            walletId,
                                                            (decimal)item.Offset,
                                                            gasPriceGwei: 1
                                                        );
                        var transactionId = transaction.TransactionHash;
                        item.EmissionTransactionId = transactionId;

                        var updateResult = await _supplyChainPartnerService.UpdateScpCredits(item.SupplyChainPartnerId, item.Offset);
                        if (updateResult)
                        {
                            item.IsProcessed = true;
                            await _actionService.SaveCarbonOffsettingAction(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while executing Job" + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing Job:" + ex);
            }
        }
    }
}
