using CochainAPI.Model.Product;

namespace CochainAPI.Model.Documents
{
    public class Contract : BaseDocument
    {
        public Guid ProductLifeCycleCategoryId { get; set; } 
        public ProductLifeCycleCategory ProductLifeCycleCategory { get; set; }
    }
}
