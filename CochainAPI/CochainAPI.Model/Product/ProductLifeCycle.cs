using System.Text.Json.Serialization;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Model.Product
{
    public class ProductLifeCycle: Base
    {
        public DateTime Timestamp { get; set; }
        public float Emissions { get; set; }
        public bool IsEmissionProcessed { get; set; }
        public string? EmissionTransactionId { get; set; }
        public Guid ProductLifeCycleCategoryId { get; set; }
        public ProductLifeCycleCategory? ProductLifeCycleCategory { get; set; }
        public Guid SupplyChainPartnerId { get; set; }
        public SupplyChainPartner? SupplyChainPartner { get; set; }
        public Guid ProductInfoId { get; set; }
        [JsonIgnore]
        public ProductInfo? ProductInfo { get; set; }
        public List<ProductLifeCycleDocument>? ProductLifeCycleDocuments { get; set; }
    }
}
