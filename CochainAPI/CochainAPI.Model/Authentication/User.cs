using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Model.Authentication
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool isActive { get; set; }
        public List<UserTemporaryPassword>? TemporaryPasswords { get; set; }

    }
}
