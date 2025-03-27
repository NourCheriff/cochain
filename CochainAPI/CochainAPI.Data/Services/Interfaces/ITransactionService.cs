using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>?> GetTransactionsByWalletId(string walletId);
        Task<Transaction?> AddTransaction(Transaction transactionObj);
    }
}
