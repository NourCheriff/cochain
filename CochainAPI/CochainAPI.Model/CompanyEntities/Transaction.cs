using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CochainAPI.Model.CompanyEntities
{
    [PrimaryKey(nameof(TransactionHash))]
    public class Transaction
    {
        public string TransactionHash { get; set; }
        public string WalletIdEmitter { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? SupplyChainPartnerEmitter { get; set; }
        public string WalletIdReceiver { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? SupplyChainPartnerReceiver { get; set; }
        [NotMapped]
        public string? SupplyChainPartnerEmitterName { get; set; }
        [NotMapped]
        public string? SupplyChainPartnerReceiverName { get; set; }
    }
}
