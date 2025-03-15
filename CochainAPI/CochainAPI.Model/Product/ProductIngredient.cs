using System.Text.Json.Serialization;

namespace CochainAPI.Model.Product
{
    public class ProductIngredient
    {
        public Guid IngredientId { get; set; }
        [JsonIgnore]  
        public ProductInfo? Ingredient { get; set; }
        [JsonIgnore]
        public Guid ProductInfoId { get; set; }
        [JsonIgnore]
        public ProductInfo? ProductInfo { get; set; }
    }
}
