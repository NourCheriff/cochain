using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductDocumentLifeCycleDocument : Document
    {
        public Guid DocumentId { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
