namespace CochainAPI.Model.Product
{
    public class ProductIngredient
    {
        public Guid IngredientId { get; set; }
        public Product Ingredient { get; set; }
        public Guid ProductInfoId { get; set; }
        public ProductInfo? ProductInfo { get; set; }
    }
}
