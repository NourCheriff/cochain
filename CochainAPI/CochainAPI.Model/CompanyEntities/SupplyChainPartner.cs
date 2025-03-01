
namespace CochainAPI.Model.CompanyEntities
{
    public class SupplyChainPartner : Base
    {
        public float Credits { get; set; }
        public string SupplyChainPartnerTypeId { get; set; }
        public SupplyChainPartnerType SupplyChainPartnerType { get; set; }
    }
}
