
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.CarbonOffset
{
    public class CarbonOffsettingAction
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public float Offset { get; set; }
        public SupplyChainPartner SupplyChainPartner { get; set; }
    }
}
