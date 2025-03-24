using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class ProductLifeCycleDocument : BaseDocument
    {
        public Guid ProductLifeCycleId { get; set; }
        public ProductLifeCycle? ProductLifeCycle { get; set; }
    }
}
