
using CochainAPI.Model.CarbonOffset;
using CochainAPI.Model.Documents;

namespace CochainAPI.Model.CompanyEntities
{
    public class SupplyChainPartner : Company
    {
        public float Credits { get; set; }
        public Guid SupplyChainPartnerTypeId { get; set; }
        public SupplyChainPartnerType? SupplyChainPartnerType { get; set; }
        public List<Contract>? ReceivedContract { get; set; }
        public List<ProductDocument>? ReceivedProductDocument { get; set; }
        public List<ProductLifeCycleDocument>? ReceivedProductLifeCycleDocument { get; set; }
        public List<SupplyChainPartnerCertificate>? ReceivedSupplyChainPartnerCertificate { get; set; }
        public List<CarbonOffsettingAction>? CarbonOffsettingActions { get; set; }
    }
}
