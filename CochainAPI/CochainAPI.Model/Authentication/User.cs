using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CochainAPI.Model.Authentication
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public bool isActive { get; set; }
    }
}
