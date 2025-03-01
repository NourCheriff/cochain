namespace CochainAPI.Model.Product
{
    public class Product: Base
    {
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}