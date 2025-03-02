
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.CarbonOffset
{
    public class CarbonOffsettingAction: Base
    {
        public float Offset { get; set; }
        public Guid SupplyChainPartnerId { get; set; }
        public SupplyChainPartner SupplyChainPartner { get; set; }
    }
}
