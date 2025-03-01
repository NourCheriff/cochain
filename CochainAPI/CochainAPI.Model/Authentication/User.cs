using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Model.Authentication
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string WalletId { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public List<UserTemporaryPassword>? TemporaryPasswords { get; set; }
        public List<IdentityUserRole<string>>? UserRoles { get; set; }
        public List<IdentityUserClaim<string>>? UserClaims { get; set; }

    }
}
