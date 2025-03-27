
using System.Text.Json.Serialization;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.CarbonOffset
{
    public class CarbonOffsettingAction: Base
    {
        public float Offset { get; set; }
        public Guid SupplyChainPartnerId { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? SupplyChainPartner { get; set; }
        public bool IsProcessed { get; set; }
        public string? EmissionTransactionId { get; set; }
    }
}
