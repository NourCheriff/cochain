using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CochainAPI.Model.CompanyEntities
{
    [PrimaryKey(nameof(TransactionHash))]
    public class Transaction
    {
        public string TransactionHash { get; set; }
        public string WalletIdEmitter { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? supplyChainPartnerEmitter { get; set; }
        public string WalletIdReceiver { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? supplyChainPartnerReceiver { get; set; }
    }
}
