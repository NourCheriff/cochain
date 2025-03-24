using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;

namespace CochainAPI.Model.Product
{
    public class ProductInfo : Base
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public Guid SupplyChainPartnerId { get; set; }
        public String? TokenId { get; set; }
        public SupplyChainPartner? SupplyChainPartner { get; set; }
        public List<ProductIngredient>? Ingredients { get; set; }
        public List<ProductLifeCycle>? ProductLifeCycles { get; set; }
        public List<ProductDocument>? ProductDocuments { get; set; }
    }
}
