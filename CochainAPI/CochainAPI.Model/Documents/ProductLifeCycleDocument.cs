using System.Text.Json.Serialization;
using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductLifeCycleDocument : BaseDocument
    {
        public Guid ProductLifeCycleId { get; set; }
        [JsonIgnore]
        public ProductLifeCycle? ProductLifeCycle { get; set; }
    }
}
