using CochainAPI.Model.Authentication;
using CochainAPI.Model.CompanyEntities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CochainAPI.Model.Documents
{
    public abstract class BaseDocument : Base
    {
        public BaseDocument() { }
        public string? Path { get; set; }
        public string? Hash { get; set; }
        public string? Type { get; set; }
        [DefaultValue("3542da56-0de3-4797-a059-effff257f63d")]
        public string? UserEmitterId { get; set; }
        [DefaultValue("d65e685f-8bdd-470b-a6b8-c9a62e39f095")]
        public Guid? SupplyChainPartnerReceiverId { get; set; }
        [JsonIgnore]
        public User? UserEmitter { get; set; }
        [JsonIgnore]
        public SupplyChainPartner? SupplyChainPartnerReceiver { get; set; }
        [DefaultValue("SGVsbG8gV29ybGQh")]
        [NotMapped]
        public string? FileString { get; set; }
        [JsonIgnore]
        [NotMapped]
        public byte[]? File
        {
            get
            {
                return Convert.FromBase64String(FileString);
            }
        }
        [NotMapped]
        public string? UserEmitterName { get; set; }
        [NotMapped]
        public string? SupplyChainPartnerReceiverName { get; set; }
        //IFormFile File { get; set; }
    }
}
