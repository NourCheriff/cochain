
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.Product
{
    public class ProductLifeCycle
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public float Emissions { get; set; }
        public ProductLifeCycleCategory ProductLifeCycleCategory { get; set; }
        public SupplyChainPartner SupplyChainPartner { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
