using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<Transaction>?> GetTransactionsByWalletId(string walletId)
        {
            if (string.IsNullOrEmpty(walletId))
                return null;
            
            return await _transactionRepository.GetTransactionsByWalletId(walletId);
        }

        public async Task<Transaction?> AddTransaction(Transaction transactionObj)
        {
            return await _transactionRepository.AddTransaction(transactionObj);
        }
    }
}