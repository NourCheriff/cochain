using System.ComponentModel;

namespace CochainAPI.Model.Authentication
{
    public class AuthenticateRequest
    {
        [DefaultValue("System")]
        public required string Username { get; set; }

        [DefaultValue("System")]
        public required string UserId { get; set; }

        [DefaultValue("System")]
        public required string Password { get; set; }
    }
}
