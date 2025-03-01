
namespace CochainAPI.Model.CompanyEntities
{
    public class SupplyChainPartner : Company
    {
        public float Credits { get; set; }
        public Guid SupplyChainPartnerTypeId { get; set; }
        public SupplyChainPartnerType SupplyChainPartnerType { get; set; }
    }
}
