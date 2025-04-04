﻿using System.Text.Json.Serialization;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Model.Authentication
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string? Role { get; set; }
        public Guid? CertificationAuthorityId { get; set; }
        public CertificationAuthority? CertificationAuthority { get; set; }
        public Guid? SupplyChainPartnerId { get; set; }
        public SupplyChainPartner? SupplyChainPartner { get; set; }
        public List<Contract>? EmittedContract { get; set; }
        public List<ProductDocument>? EmittedProductDocument { get; set; }
        public List<ProductLifeCycleDocument>? EmittedProductLifeCycleDocument { get; set; }
        public List<SupplyChainPartnerCertificate>? EmittedSupplyChainPartnerCertificate { get; set; }
        [JsonIgnore]
        public List<UserTemporaryPassword>? TemporaryPasswords { get; set; }
        public List<IdentityUserRole<string>>? UserRoles { get; set; }
        public List<IdentityUserClaim<string>>? UserClaims { get; set; }
        [JsonIgnore]
        public List<Log>? Logs { get; set; }
    }
}
