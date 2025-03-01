using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.Product
{
    public class ProductInfo: Base
    {
        public Product? Product { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public SupplyChainPartner? SupplyChainPartner { get; set; }
        public List<ProductIngredient> Ingredients { get; set; }
    }
}
