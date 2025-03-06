using CochainAPI.Model.Authentication;
using CochainAPI.Model.CompanyEntities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
