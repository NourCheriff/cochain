using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface ITransactionRepository
    {  
        Task<List<Transaction>?> GetTransactionsByWalletId(string walletId);
        Task<Transaction?> AddTransaction(Transaction transactionObj);
    }
}
