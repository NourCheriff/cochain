using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductDocument : Document
    {
        public Guid DocumentId { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
