using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductDocument : BaseDocument
    {
        public Guid ProductInfoId { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
