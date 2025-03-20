using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model.Transaction
{
    [PrimaryKey(nameof(TransactionHash))]
    public class EmissionTransaction
    {
        public string TransactionHash { get; set; }
        public string WalletIdEmitter { get; set; }
        public string WalletIdReceiver { get; set; }
    }
}
