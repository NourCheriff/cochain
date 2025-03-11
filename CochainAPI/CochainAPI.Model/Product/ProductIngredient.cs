using System.Text.Json.Serialization;

namespace CochainAPI.Model.Product
{
    public class ProductIngredient
    {
        public Guid IngredientId { get; set; }
        public ProductInfo? Ingredient { get; set; }
        public Guid ProductInfoId { get; set; }
        [JsonIgnore]
        public ProductInfo? ProductInfo { get; set; }
    }
}
