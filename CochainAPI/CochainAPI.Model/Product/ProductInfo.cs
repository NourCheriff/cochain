using CochainAPI.Model.Company;

namespace CochainAPI.Model.Product
{
    public class ProductInfo: Base
    {
        public Product Product { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public Company SupplyChainPartner { get; set; }
    }
}
