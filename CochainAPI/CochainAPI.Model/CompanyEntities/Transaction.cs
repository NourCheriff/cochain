using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model.CompanyEntities
{
    [PrimaryKey(nameof(TransactionHash))]
    public class Transaction
    {
        public string TransactionHash { get; set; }
        public string WalletIdEmitter { get; set; }
        public SupplyChainPartner? SupplyChainPartnerEmitter { get; set; }
        public string WalletIdReceiver { get; set; }
        public SupplyChainPartner? SupplyChainPartnerReceiver { get; set; }
    }
}
