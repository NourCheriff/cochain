using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductLifeCycleDocument : BaseDocument
    {
        public Guid ProductInfoId { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
