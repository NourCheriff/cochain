using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class TransactionRepository : SqlRepository, ITransactionRepository
    {
        private readonly ILogRepository logRepository;
        public TransactionRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<List<Transaction>?> GetTransactionsByWalletId(string walletId)
        {
            return await dbContext.Transaction.Where(x => x.WalletIdEmitter == walletId || x.WalletIdReceiver == walletId)
                .Include(x => x.SupplyChainPartnerReceiver)
                .Include(x => x.SupplyChainPartnerEmitter)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Transaction?> AddTransaction(Transaction transactionObj)
        {
            var savedTransaction = await dbContext.Transaction.AddAsync(transactionObj);
            await dbContext.SaveChangesAsync();
            transactionObj.TransactionHash = savedTransaction.Entity.TransactionHash;
            return transactionObj;
        } 
    }
}