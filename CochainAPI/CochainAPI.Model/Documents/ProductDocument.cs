using System.Text.Json.Serialization;
using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductDocument : BaseDocument
    {
        public Guid ProductInfoId { get; set; }
        [JsonIgnore]
        public ProductInfo? ProductInfo { get; set; }
    }
}
