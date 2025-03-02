using CochainAPI.Model.Authentication;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Model.Documents
{
    public abstract class BaseDocument: Base
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public string UserEmitterId { get; set; }
        public Guid SupplyChainPartnerReceiverId { get; set; }
        public User UserEmitter { get; set; }
        public SupplyChainPartner SupplyChainPartnerReceiver { get; set; }
    }
}
